export class MyComment {
    constructor(id, text, color) {
        this.id = id;
        this.text = text;
        this.color = color;
    }
    createCommentBlock(container) {        
        // ������� ��������� ����        
        var input = document.createElement('input');
        input.id = this.id;
        input.type = 'text';
        input.value = this.text;
        input.style.backgroundColor = this.color;
        input.style.width = 'fit-content';
        //��������� �������� ����������� ������ � ��
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
        // ��������� ��������� ���� � ���������
        container.appendChild(input);
        //nd.appendChild(input);
        //container.appendChild(nd);
    }
}