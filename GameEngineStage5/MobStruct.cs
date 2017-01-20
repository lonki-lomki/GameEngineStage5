namespace GameEngineStage5
{
    public struct MobStruct
    {
        public string name;
        public float speed;
        public float hp;
        public float damage;
        public float exp;
        //public string tile_type;

        public override string ToString()
        {
            string res = "<mobName:" + name + ">";
            res += "<mobSpeed:" + speed + ">";
            res += "<mobHP:" + hp + ">";
            res += "<mobDamage:" + damage + ">";
            res += "<mobExp:" + exp + ">";
            return res;
        }
    }

}
