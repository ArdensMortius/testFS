using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class MyComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Text { get; set; }
        [ForeignKey("MyDotId")]        
        public int MyDotID { get; set; }      
        public MyDot Dot { get; set; }
        public int intcolor { get; set; }
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public MyComment() { }
        public MyComment(int dotID, string? text = null, int intcolor = 16777215)
        {
            this.MyDotID = dotID;
            if (text is null) this.Text = "";
            else this.Text = text;
            this.intcolor = intcolor;
        }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
    }
}
