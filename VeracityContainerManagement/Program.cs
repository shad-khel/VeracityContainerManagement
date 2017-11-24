using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace VeracityContainerManagement
{
    class Program
    {

         class Person
        {
            public string DNVGLID { get; set; }
            public string Name { get; set; } 
            public string Email { get; set; }
           
        }

        class Container
        {
            public string ResourceId { get; set; }
            public string Name { get; set; }
          
        }
         

        List<Person> People = new List<Person>();
        List<Container> Containers = new List<Container>();
        List<UserGroup> UserGroup = new List<UserGroup>();
        
        static void Main(string[] args)
        {


        }

        //Commands

        //TODO Add user
         void AddUser(Person p)
        {
            if (!People.Exists(o => o.DNVGLID == p.DNVGLID))
                People.Add(p);

        }

        //TODO Add container
        void AddContainer(Container c)
        {
            if (!Containers.Exists(o => o.ResourceId == c.ResourceId) )
                Containers.Add(c);
            
        }



        //TODO Add user to list
        //TODO Add container to list
        //TODO remove user from list
        //TODO remove container from list
        //TODO Add User to shared container list
        //TODO remove User to shared container list
        //TODO Add Container to shared user list
        //TODO Remove container from shared container list



    }
}
