[System.Serializable]
public class CharacterSaveData
{
    public char character;
    public bool hint;
    public bool completed;

    public CharacterSaveData()
    {
        
    }

    public CharacterSaveData(char character)
    {
        this.character = character;
    }

    public CharacterSaveData(char character,bool hint,bool completed)
    {
        this.character = character;
        this.hint = hint;
        this.completed = completed;
    }
    
    public void Hint()
    {
        hint = true;
        completed = true;
    }

    public void Completed()
    {
        completed = true;
    }

    public int GetPoint()
    {
        if (!hint)
        {
            return 1;
        }

        return 0;
    }
}