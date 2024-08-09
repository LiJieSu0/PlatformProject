
public interface IEnemy{
    float MaxHp{ get; set; }
    float MaxMp{ get; set; }
    float CurrHp{ get; set; }
    float CurrMp{ get; set; }
    string EnemyName{get; set;}
    float BasicDamage{ get; set; }

    void ReceiveDamage(float damage);
    
}