namespace Engine.Data
{
	
	public class Character
    {

        public Account Account       { get; set; } = new Account();

        public State State           { get; set; } = new State();

        public Factors Factors       { get; set; } = new Factors();

        public Parameters Parameters { get; set; } = new Parameters();

        public Skills Skills         { get; set; } = new Skills();

        public Exps Exps             { get; set; } = new Exps();

        public Equipment Equipment   { get; set; } = new Equipment();

        public Inventory Inventory   { get; set; } = new Inventory();
        
        public Quest Quest           { get; set; } = new Quest();

    }
	
}
