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
        //������ ������ � ��������� � layer
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
            // ��������� ��������� div � �������������
            var commentDiv = document.getElementById('comments-' + this.id);
            if (commentDiv) {
                commentDiv.style.left = circle.x() + 'px';
                commentDiv.style.top = (circle.y() + this.radius+10) + 'px';
            }            
        });

        circle.on('dragend', () => {            
            //���������� � ���������� ����� ���������� ����� ���������� �����������
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

        //��������� ������� ��������
        circle.on('wheel', (e) => {
            e.evt.preventDefault();
            //>0 - ��������� ����            
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
            //��������� ��������, ���� ��� ���� � ���������� �����
            var commentDiv = document.getElementById('comments-' + this.id);
            commentDiv.style.top = (circle.y() + this.radius + 10) + 'px';
            //��������� ������ � ��
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
        // �������� ������ �� ������������ div
        var existingDiv = document.getElementById('container');
        // ������� ��������� div
        var container = document.createElement('div');
        container.id = 'comments-'+this.id;
        //container.style.display = 'flex';
        // ������������� ���������� ����������
        container.style.position = 'absolute';
        container.style.left = this.cordx + 'px'; // ���������� x
        container.style.top = (this.cordy + this.radius+10) + 'px'; // ���������� y
        container.style.transform = 'translateX(-50%)';
        container.style.display = 'flex';
        container.style.flexDirection = 'column';
        container.style.justifyContent = 'center';
        //��������� ������ ������� ������� ��������
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
            //���������� ������ � ����� div
            var button = document.getElementById("addCommentButton" + this.id);
            container.removeChild(button);            
            container.appendChild(button);
        })
        // ��������� ��������� � ������������ div
        existingDiv.appendChild(container);
        // ��������� ����������� text input � ���������
        this.comments.forEach(c => {
            //console.log('Calling createCommentBlock for comment:', c);
            c.createCommentBlock(container);            
        //��������� ������ ����� ���������
        });
        container.appendChild(b);
    }
}