namespace PetShopAdir.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string CommentText { get; set; }

        public virtual int AnimalId {  get; set; }
        public virtual Animal Animal { get; set; }

    }
}
