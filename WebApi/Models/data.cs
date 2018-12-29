using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{

    public class ElmData
    {
        public ObjectId _id { get; set; }
        public Seller seller { get; set; }
        public List<Good> goods { get; set; }
        public List<Rating> ratings { get; set; }
    }

    public class Seller
    {
        public string name { get; set; }
        public string description { get; set; }
        public int deliveryTime { get; set; }
        public float score { get; set; }
        public float serviceScore { get; set; }
        public float foodScore { get; set; }
        public float rankRate { get; set; }
        public int minPrice { get; set; }
        public int deliveryPrice { get; set; }
        public int ratingCount { get; set; }
        public int sellCount { get; set; }
        public string bulletin { get; set; }
        public Support[] supports { get; set; }
        public string avatar { get; set; }
        public string[] pics { get; set; }
        public string[] infos { get; set; }
    }

    public class Support
    {
        public int type { get; set; }
        public string description { get; set; }
    }

    public class Good
    {
        public string name { get; set; }
        public int type { get; set; }
        public Food[] foods { get; set; }
    }

    public class Food
    {
        public string name { get; set; }
        public int price { get; set; }
        public object oldPrice { get; set; }
        public string description { get; set; }
        public int sellCount { get; set; }
        public object rating { get; set; }
        public string info { get; set; }
        public Rating[] ratings { get; set; }
        public string icon { get; set; }
        public string image { get; set; }
    }
    public class Rating
    {
        public string username { get; set; }
        public long rateTime { get; set; }
        public object deliveryTime { get; set; }
        public int score { get; set; }
        public int rateType { get; set; }
        public string text { get; set; }
        public string avatar { get; set; }
        public string[] recommend { get; set; }
    }

}
