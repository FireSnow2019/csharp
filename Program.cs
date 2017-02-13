using System;
using System.Collections.Generic;
using DbConnection;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool resultFlag = false;
            getUserList();

            Console.Write("Choose your option: \n 1 for SELECT \n 2 for INSERT \n 3 for UPDATE \n 4 for DELETE \n\n");
            int optionselected = Convert.ToInt16(Console.ReadLine());
            
            if (optionselected == 2)
            {
                //Insert 
                resultFlag = InsertUser();
            }
            else if (optionselected == 3)
            {
                //Update
                Console.Write("Update selected\n_______________\n");
                resultFlag = UpdateUser();
            }
            else if (optionselected == 4)
            {
                //Delete
                Console.Write("Delete selected\n_______________\n");
                resultFlag = DeleteUser();
            }
            else{
                //select
                resultFlag = getUserList();
            }

            //Result of my action with database:

            if (resultFlag)
            { 
                Console.Write("Success!\n\n");
            }
            else
            {
                Console.Write("Failed!\n\n");
            }
        }


        public static bool getUserList()
        {
            
                string selectQry = "select * from Users";
                foreach(var usr in DbConnector.ExecuteQuery(selectQry))
                {
                    Console.WriteLine("ID: {0} \n", usr["ID"]);
                    Console.WriteLine("Name: {0} {1} \n ", usr["FirstName"], usr["LastName"]);
                    Console.WriteLine("Favorite Number: {0} \n ", usr["FavoriteNumber"]);
                    Console.WriteLine("------------------\n");
                }
                //DbConnector.ExecuteQuery(selectQry);
                 
              return true;  
           
        }

        public static bool InsertUser()
        {
            try{
                //DbConnector.ExecuteQuery("select * from Users");
                Console.WriteLine("Enter first name: ");
                string fname = Console.ReadLine();
                Console.WriteLine("Enter last name: ");
                string lname = Console.ReadLine();
                Console.WriteLine("Enter your favorite number (1-10): ");
                int favNum = Convert.ToInt16(Console.ReadLine());
                string strQuery = $"INSERT Users(FirstName,LastName,FavoriteNumber) VALUES ('{fname}','{lname}',{favNum})";
                DbConnector.ExecuteQuery(strQuery);
                
            }
            catch(Exception e)
            {
                Console.WriteLine("error Occured at {0}",e.ToString());
                return false;
            }

            return true;
        }

//Update user function
        public static bool UpdateUser()
        {
            try{
                Console.Write("Enter ID to update");
                string userid = Console.ReadLine();
                if(userid != null || userid != "")
                {  
                    Console.WriteLine("Enter first name: ");
                    string fname = Console.ReadLine();
                    
                    Console.WriteLine("Enter last name: ");
                    string lname = Console.ReadLine();
                    Console.WriteLine("Enter your favorite number (1-10): ");
                    int favNum = Convert.ToInt16(Console.ReadLine());
                    
                    string strQuery = $"UPDATE Users SET FirstName='{fname}',LastName='{lname}',FavoriteNumber='{favNum}' WHERE ID = {userid}";
                    DbConnector.ExecuteQuery(strQuery);
                }
                
            }
            catch(Exception e)
            {
                Console.WriteLine("error Occured at {0}",e.ToString());
                return false;
            }

            return true;
        }
//Delete user function
        public static bool DeleteUser()
        {
            try{
                Console.WriteLine("Enter ID: ");
                int id = Convert.ToInt32(Console.ReadLine());
                if (id > 0)
                    {
                        string strQuery = $"DELETE FROM Users WHERE ID = {id}";
                        DbConnector.ExecuteQuery(strQuery);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                
            }
            catch(Exception e)
            {
                Console.WriteLine("error Occured at {0}",e.ToString());
                return false;
            }
        }

    }
}
