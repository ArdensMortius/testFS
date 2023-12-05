using System.Drawing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class MyDot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int Cord_x { get; set; }
        public int Cord_y { get; set; }
        public int Radius { get; set; }
        public ICollection<MyComment> Comments { get; set; }
        public int ARGBColor { get; set; }
        //[NotMapped]
        //public Color ColorSettings { get => Color.FromArgb(intcolor); set => intcolor = value.ToArgb(); }

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public MyDot() { }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public MyDot(int x, int y, int radius = 5, Color? color = null)
        {
            Cord_x = x;
            Cord_y = y;
            Radius = radius;
            Comments = new List<MyComment>();
            if (color is null) ARGBColor = Color.DarkGray.ToArgb();
            else ARGBColor = ((Color)color).ToArgb();
        }
    }
}
