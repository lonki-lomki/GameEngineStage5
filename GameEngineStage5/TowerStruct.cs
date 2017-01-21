namespace GameEngineStage5
{
    public struct TowerStruct
    {
        //public string towerType;
        public string name;
        public string description;
        public string element;
        public float damage;
        public float damage_radius;
        public float damage_freq;
        public float bulletSpeed;
        public string tile_type;
        public float cost;
        public string nextTowerType;

        public override string ToString()
        {
            string res = "<towerName:" + name + ">";
            res += "<towerDescription:" + description + ">";
            res += "<towerElement:" + element + ">";
            res += "<towerDamage:" + damage + ">";
            res += "<towerDamageRadius:" + damage_radius + ">";
            res += "<towerDamageFreq:" + damage_freq + ">";
            res += "<towerBulletSpeed:" + bulletSpeed + ">";
            res += "<towerTileType:" + tile_type + ">";
            res += "<towerCost:" + cost + ">";
            res += "<towerNextType:" + nextTowerType + ">";
            return res;
        }
    }
}
