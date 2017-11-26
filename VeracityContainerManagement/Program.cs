using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace VeracityContainerManagement
{
    class Program
    {
         

        public  class Storage
        {

            public  List<Modles.Person> People = new List<Modles.Person>();
            public  List<Modles.Container> Containers = new List<Modles.Container>();
            public  List<Modles.UserGroup> UserGroups = new List<Modles.UserGroup>();
            public  List<Modles.ContainerGroup> ContainerGroups = new List<Modles.ContainerGroup>();
            public  List<Modles.UserGroupContainerGroupAccess> UserGroupContainerGroupAccesses = new List<Modles.UserGroupContainerGroupAccess>();
            
        }


        static void Main(string[] args)
        {
            //SetUp
            Storage s = new Storage();
            Commands c = new Commands(s);
            Queries q = new Queries(s);

            var shad = new Modles.Person { Email = "shad.khel@dnvgl.com", DNVGLID = Guid.NewGuid().ToString(), Id = 1, Name = "Shad" };
            var anatoliy = new Modles.Person { Email = "Anatoliy.Tanaschuk@dnvgl.com", DNVGLID = Guid.NewGuid().ToString(), Id = 2, Name = "Anatoliy" };
            var s1 = new Modles.Container { Id = 1, Name = "ship_01", ResourceId = Guid.NewGuid().ToString() };
            var s2 = new Modles.Container { Id = 2, Name = "ship_02", ResourceId = Guid.NewGuid().ToString() };


            c.AddUser(shad);
            c.AddUser(anatoliy);

            c.AddContainer(s1);
            c.AddContainer(s2);

            //Add all ships containers to container grouping ships
            var cg1 = new Modles.ContainerGroup { Id = 1, name = "ships", ContainerInGroup = new List<Modles.Container>() };
            c.AddContainerToContainerGroup(s1, cg1);
            c.AddContainerToContainerGroup(s2, cg1);

            
            //Create user groups to access ships
            var ug1 = new Modles.UserGroup { Id = 1, name = "readers of ships", PersonInGroup = new List<Modles.Person>() };
            var ug2 = new Modles.UserGroup { Id = 2, name = "admin of ships", PersonInGroup = new List<Modles.Person>() };
            

            //Add users to a user group
            c.AddPersonToUserGroup(shad, ug1);
            c.AddPersonToUserGroup(shad, ug2);
            c.AddPersonToUserGroup(anatoliy, ug2);

            //Give access to ship
            c.AddContainerGroupToUserGroup(cg1, ug1);
            c.AddContainerGroupToUserGroup(cg1, ug2);

            //Which group has access?
            var o = q.getListOfUserGroupsWithAcessToContainerGroup(cg1);
            

            Console.WriteLine($"Which group has access to {cg1.name}");
            Console.WriteLine(o.Count);
            foreach (var aa in o)
            {
                Console.WriteLine(aa.name);

                Console.WriteLine("whos in that group?");
                var p = q.getListOfPeapleInGroupQuery(aa);
                foreach (var pp in p)
                {
                    Console.WriteLine(pp.Name);
                }
            }

            Console.Read();
        }

        public class Modles
        {
            public class Person
            {
                public int Id { get; set; }
                public string DNVGLID { get; set; }
                public string Name { get; set; }
                public string Email { get; set; }

            }

            public class Container
            {
                public int Id { get; set; }
                public string ResourceId { get; set; }
                public string Name { get; set; }

            }

            public class UserGroup
            {
                public int Id { get; set; }
                public string name { get; set; }
                public List<Person> PersonInGroup { get; set; }
            }

            public class ContainerGroup
            {
                public int Id { get; set; }
                public string name { get; set; }
                public List<Container> ContainerInGroup { get; set; }



            }
            public class UserGroupContainerGroupAccess
            {
                public int Id { get; set; }
                public UserGroup UserGroup { get; set; }
                public ContainerGroup ContainerGroup { get; set; }
            }
        }


        public class Commands
        {
            Storage _store;

            public Commands(Storage storage)
            {
                _store = storage;
            }
             
            public void AddUser(Modles.Person p)
            {
                if (!_store.People.Exists(o => o.DNVGLID == p.DNVGLID))
                    _store.People.Add(p);

            }


            public void AddContainer(Modles.Container c)
            {
                if (!_store.Containers.Exists(o => o.ResourceId == c.ResourceId))
                    _store.Containers.Add(c);

            }


            public void AddPersonToUserGroup(Modles.Person p, Modles.UserGroup ug)
            {
                if (!ug.PersonInGroup.Contains(p))
                {
                    ug.PersonInGroup.Add(p);
                }
            }


            public void AddContainerToContainerGroup(Modles.Container c, Modles.ContainerGroup cg)
            {
                if (!cg.ContainerInGroup.Contains(c))
                {
                    cg.ContainerInGroup.Add(c);
                }
            }


            public void RemovePersonToUserGroup(Modles.Person p, Modles.UserGroup ug)
            {
                if (ug.PersonInGroup.Contains(p))
                {
                    ug.PersonInGroup.Add(p);
                }
            }


            public void RemoveContainerToContainerGroup(Modles.Container c, Modles.ContainerGroup cg)
            {
                if (cg.ContainerInGroup.Contains(c))
                {
                    cg.ContainerInGroup.Add(c);
                }
            }

            //TODO Add User to shared container list


            //TODO remove User to shared container list

            //TODO Add Container to shared user list
            public void AddContainerGroupToUserGroup(Modles.ContainerGroup cg, Modles.UserGroup ug)
            {

                // This one is difficult to check if exsits

                _store.UserGroupContainerGroupAccesses.Add(new Modles.UserGroupContainerGroupAccess { ContainerGroup = cg, UserGroup = ug });

            }

            //TODO Remove container from shared container list

        }

        public class Queries
        {
            Storage _store;

            public Queries(Storage storage)
            {
                _store = storage;
            }

            //Who is in user group X
            public List<Modles.Person> getListOfPeapleInGroupQuery(Modles.UserGroup ug)
            {
                return ug.PersonInGroup;
            }

            //What containers are in container group X
            public List<Modles.Container> getListOfContainersInContainerGroupQuery(Modles.ContainerGroup cg)
            {
                return cg.ContainerInGroup;
            }

            //Which group has access to this container group
            public List<Modles.UserGroup> getListOfUserGroupsWithAcessToContainerGroup(Modles.ContainerGroup cg)
            {

                List<Modles.UserGroup> result = new List<Modles.UserGroup>();

                var access =_store.UserGroupContainerGroupAccesses.FindAll(o => o.ContainerGroup.Id == cg.Id);
                foreach ( var v in access)
                {
                    if (!result.Exists(p => p.Id == v.UserGroup.Id))
                    {
                        result.Add(v.UserGroup);
                    }
                }

                return result;

            }

            //TODO Implement user access - Who has aceess to this container

        }
    }
}
