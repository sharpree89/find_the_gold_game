using Nancy;
using System;
using System.Collections.Generic; 

namespace NG1
{
    public class NGModule : NancyModule
    {
        public NGModule()
        {
            Get("/", args => 
            {
            if(Session["gold"] == null)
            {
                Session["gold"] = 0;
                ViewBag.log = "";
            }
            ViewBag.gold = Session["gold"];
            ViewBag.log = Session["log"]; 
            return View["NG.sshtml"];
            });

            Post("/process_money", args =>
            {
                Random rnd = new Random();
                string building = Request.Form["building"];     

                if(building == "farm"){
                    int farmGold = (int)rnd.Next(10,21);
                    Session["gold"] = (int)Session["gold"] + farmGold;
                    Session["log"] += $"<p> You found {farmGold} gold on the farm!</p>";
                } 
                if(building == "cave"){
                    int caveGold = (int)rnd.Next(5,11);
                    Session["gold"] = (int)Session["gold"] + caveGold;
                    Session["log"] += $"<p> You found {caveGold} gold in the cave!</p>";
                }
                if(building == "house"){
                    int houseGold = (int)rnd.Next(2,6);
                    Session["gold"] = (int)Session["gold"] + houseGold;
                    Session["log"] += $"<p> You found {houseGold} gold in the house!</p>";
                }
                if(building == "casino"){
                    int casinoGold = (int)rnd.Next(-50,51);
                    if(casinoGold > 0)    
                    {
                        Session["gold"] = (int)Session["gold"] + casinoGold;
                        Session["log"] += $"<p> You found {casinoGold} gold in the casino!</p>";
                    }
                    else if(casinoGold < 0)    
                    {
                        Session["gold"] = (int)Session["gold"] + casinoGold;
                        Session["log"] += $"<p> You lost {casinoGold} gold in the casino!</p>";
                    }
                    else if(casinoGold == 0)
                    {
                        Session["log"] += $"<p> You broke even - {casinoGold} gold found at the casino! </p>";
                    }
                }
                return Response.AsRedirect("/");
            });

            Post("/clear", args =>
            {
                Session.DeleteAll();
                return Response.AsRedirect("/");
            });
        }
    }
}