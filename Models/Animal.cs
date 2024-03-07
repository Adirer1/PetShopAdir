namespace PetShopAdir.Models
{
    public class Animal
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string PictureName { get; set; }
        public string Description { get; set; }
        //navigation

        public virtual int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }


    }
}
