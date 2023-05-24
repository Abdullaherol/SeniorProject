[System.Serializable]
public class CharacterSaveData//Each Character Data Class
{
    public char character;
    public bool hint;
    public bool completed;

    public CharacterSaveData()//Constructor
    {
        
    }

    public CharacterSaveData(char character)//Constructor
    {
        this.character = character;
    }

    public CharacterSaveData(char character,bool hint,bool completed)//Constructor
    {
        this.character = character;
        this.hint = hint;
        this.completed = completed;
    }
    
    public void Hint()//Set hint
    {
        hint = true;
        completed = true;
    }

    public void Completed()//set completed
    {
        completed = true;
    }

    public int GetPoint()//get point of character
    {
        if (!hint)
        {
            return 1;
        }

        return 0;
    }
}