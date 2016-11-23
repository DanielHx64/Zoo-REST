using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Zoo.Models
{
    public class AnimalsCl
    {
        private int ID;
        private String name;
        private String DOB;

        //The constructers
        public AnimalsCl(int nID, String nName, String nDOB)
        {
            ID = nID;
            name = nName;
            DOB = nDOB;
        }

        public AnimalsCl(int nID)
        {
            ID = nID;
        }


        //Getters and Setters, will not allow the re/setting of an ID outside the constructor
        public int getID()
        {
            return ID;
        }

        public String getName()
        {
            return name;
        }

        public void setName(String nName)
        {
            name = nName;
        }

        public String getDOB()
        {
            return DOB;
        }

        public void setDOB(String nDOB)
        {
            DOB = nDOB;
        }

        //validate the DOB
        private bool validateSetDOB(String DOB)
        {
            //gotten from https://forums.asp.net/t/1230398.aspx?how+can+i+validate+the+dateformat+mm+dd+yyyy+in+a+textbox+using+C+

            DateTime time;
            if (DateTime.TryParseExact(DOB, "dd/MM/yyyy", null, DateTimeStyles.None, out time) == true){
                setDOB(DOB);
                return true;
            }else{
                return false;
            }

            //catch all return 
            return false;
        }
    }

    
}