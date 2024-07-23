
public interface IEnemy{
    float MaxHp{ get; set; }
    float MaxMp{ get; set; }
    float CurrHP{ get; set; }
    float CurrMp{ get; set; }
    string MobName{get; set;}
    float BasicDamage{ get; set; }

    void ReceiveDamage(float damage);
    void DealDamage(float damage);
}