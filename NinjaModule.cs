using Nancy;
using System;
using System.Collections.Generic;

namespace NinjaNancy
{
    public class NinjaModule : NancyModule
    {
        public NinjaModule()
        {
            Get("/", args => {
                if(Session["activities"] == null)
                {
                    Session["activities"] = 0;
                    ViewBag.display = "";
                }
                ViewBag.activities = Session["activities"];
                ViewBag.display = Session["display"];
                return View["Ninja"];
            });
            Post("/process_money", args =>
            {
                DateTime date = DateTime.Now;
                string building = Request.Form.building;
                Console.WriteLine(building);
                Random rnd = new Random();

                if(building == "farm")
                {
                   int Farm = (int)rnd.Next(10,21);
                   Session["activities"] = (int) Session["activities"] + Farm;
                   Session["display"] += $"<p> Earned {Farm} golds from the farm! {date} </p>";
                }

                else if(building == "cave")
                {
                    int Cave = (int)rnd.Next(5,11);
                    Session["activities"] = (int) Session["activities"] + Cave;
                    Session["display"] +=  $"<p> Earned {Cave} golds from the cave! ({date}) </p>";
                }
                else if(building == "house")
                {
                    int House = (int)rnd.Next(2,6);
                    Session["activities"] = (int) Session["activities"] + House;
                    Session["display"] +=  $"<p> Earned {House} golds from the house! ({date}) </p>";
                }
                else if (building == "casino")
                {
                    int Casino = (int)rnd.Next(-50,51);
                    Session["activities"] = (int) Session["activities"] + Casino;
                    if (Casino >= 0)
                    {
                        Session["display"] += $"<p> Entered a casino and earned {Casino} ...Yay!...({date}) </p> ";                                        
                    }
                    else
                    {
                        Session["display"] += $"<p> Entered a casino and lost {Casino}...Ouch!...({date}) </p>";                                            
                    }
                }
                else
                {
                    Session["display"] += ("something went wrong");
                }
                return Response.AsRedirect("/");
            });  
        }
    }
}
