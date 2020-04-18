namespace Shared.Definitions
{
    public class TRecipe : AbstractDefinition
    {
        public string Skill { get; set; }
        public int Difficulty { get; set; }
        public string Resource { get; set; }
        public int Yields { get; set; }
        public TRecipeTool[] Tools { get; set; }
        public TRecipeMachine[] Machines { get; set; }
        public TRecipeBuilding[] Buildings { get; set; }
    }
}
