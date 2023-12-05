export class MyComment {
    constructor(id, text, color) {
        this.id = id;
        this.text = text;
        this.color = color;
    }
    createCommentBlock(container) {        
        // Создаем текстовое поле        
        var input = document.createElement('input');
        input.id = this.id;
        input.type = 'text';
        input.value = this.text;
        input.style.backgroundColor = this.color;
        input.style.width = 'fit-content';
        //добавляем отправку обновлённого текста в бд
        input.addEventListener('change', () => {
            //console.log("Text has been changed");
            let dataToSend = {
                ID: this.id,
                Text: input.value,
            };
            $.ajax({
                url: '/Home/UpdateText',
                type: 'POST',
                data: JSON.stringify(dataToSend),
                contentType: 'application/json',
                success: function () {
                    console.log("Text saved.");
                },
                error: function () {
                    console.log("Text save error.");
                }
            });
        });
        // Добавляем текстовое поле в контейнер
        container.appendChild(input);
        //nd.appendChild(input);
        //container.appendChild(nd);
    }
}