import { MyComment } from '/js/classes/MyComment.js';
export class MyDot {
    constructor(id, x, y, color, radius, comments) {
        this.id = id;
        this.cordx = x;
        this.cordy = y;
        this.color = color;
        this.radius = radius;
        this.comments = comments;
    }

    DrawDotAndComments(layer,data) {
        //создаём кружок и добавляем в layer
        var circle = new Konva.Circle({
            x: this.cordx,
            y: this.cordy,
            radius: this.radius,
            fill: this.color,
            strokeWidth: 0,
            draggable: true,
        });

        circle.on('dblclick', () => {            
            const index = data.indexOf(this);
            if (index > -1) {
                data.splice(index, 1);
            }
            circle.remove();
            layer.draw();
            var commentDiv = document.getElementById('comments-' + this.id);
            if (commentDiv) {
                commentDiv.remove();
            }
        })

        circle.on('dragmove', () => {
            // Обновляем положение div с комментариями
            var commentDiv = document.getElementById('comments-' + this.id);
            if (commentDiv) {
                commentDiv.style.left = circle.x() + 'px';
                commentDiv.style.top = (circle.y() + this.radius+10) + 'px';
            }            
        });

        circle.on('dragend', () => {            
            //отправляем в контроллер новые координаты после завершения перемещения
            let datatosend = {
                ID: this.id,
                Cord_x: this.cordx,
                Cord_y: this.cordy,
            };
            $.ajax({
                url: '/Home/MovePoint',
                type: 'POST',
                data: JSON.stringify(datatosend),
                contentType: 'application/json',
                success: function () {
                    console.log("Move saved.");
                },
                error: function () {
                    console.log("Move error.");
                }
            });
        })

        //изменялка размера колёсиком
        circle.on('wheel', (e) => {
            e.evt.preventDefault();
            //>0 - прокрутка вниз            
            if (e.evt.deltaY < 0) {
                if (this.radius < 100) {
                    this.radius+=2;
                    circle.attrs.radius+=2;                                                
                }
            }
            else {                
                if (this.radius > 10) {
                    this.radius-=2;
                    circle.attrs.radius-=2;                    
                }
            }
            layer.draw();
            //подвинуть комменты, чтоб они были в правильном месте
            var commentDiv = document.getElementById('comments-' + this.id);
            commentDiv.style.top = (circle.y() + this.radius + 10) + 'px';
            //сохранить радиус в бд
            let datatosend = {
                ID: this.id,
                Radius: this.radius
            }
            $.ajax({
                url: '/Home/ResizePoint',
                type: 'POST',
                data: JSON.stringify(datatosend),
                contentType: 'application/json',
                success: function () {
                    console.log("New size saved.");
                },
                error: function () {
                    console.log("Resize error.");
                }
            });
        });

        layer.add(circle);
        // Получаем ссылку на существующий div
        var existingDiv = document.getElementById('container');
        // Создаем контейнер div
        var container = document.createElement('div');
        container.id = 'comments-'+this.id;
        //container.style.display = 'flex';
        // Устанавливаем координаты контейнера
        container.style.position = 'absolute';
        container.style.left = this.cordx + 'px'; // координата x
        container.style.top = (this.cordy + this.radius+10) + 'px'; // координата y
        container.style.transform = 'translateX(-50%)';
        container.style.display = 'flex';
        container.style.flexDirection = 'column';
        container.style.justifyContent = 'center';
        //добавляем кнопку спавнер пустого коммента
        var b = document.createElement('button');
        b.id = "addCommentButton" + this.id;
        b.style.fontWeight = 'bold';
        b.textContent = '(+)';
        b.style.alignContent = 'center';
        b.addEventListener('click', () => {
            let ncid = this.comments.length;
            let newcomment = new MyComment(ncid, 'newcomment', '#f0f0f0')
            this.comments[ncid] = newcomment;
            newcomment.createCommentBlock(container); 
            //перемещаем кнопку в конец div
            var button = document.getElementById("addCommentButton" + this.id);
            container.removeChild(button);            
            container.appendChild(button);
        })
        // Добавляем контейнер в существующий div
        existingDiv.appendChild(container);
        // Добавляем комментарии text input в контейнер
        this.comments.forEach(c => {
            //console.log('Calling createCommentBlock for comment:', c);
            c.createCommentBlock(container);            
        //добавляем кнопку после комментов
        });
        container.appendChild(b);
    }
}