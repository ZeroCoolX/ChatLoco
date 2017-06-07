﻿using ChatLoco.Entities.UserDTO;
using System.Collections.Generic;
using System.Timers;

namespace ChatLoco.Classes.Chatroom
{
    public class ActiveUser
    {
        public int Id { get; }
        public string UserName { get; }
        public bool IsActive { get; set; }
        public string UserHandle { get; set; }

        //parent list that the user belongs to
        //this list belongs to the chatroom that the user is in
        //we need it so we can remove ourself from the list if necessary
        private Dictionary<int, ActiveUser> BelongsToUsersList;
        private HashSet<string> BelongsToHandlesList;

        Timer IdleTimer;

        public ActiveUser(UserDTO user, string userHandle, bool isActive, Dictionary<int, ActiveUser> usersList, HashSet<string>handlesList)
        {
            Id = user.Id;
            UserName = user.Username;
            IsActive = isActive;
            BelongsToUsersList = usersList;
            UserHandle = userHandle;
            BelongsToHandlesList = handlesList;

            //create a timer that calls the IdleCheck method every 11 seconds
            IdleTimer = new Timer();
            IdleTimer.Elapsed += new ElapsedEventHandler(IdleCheck);
            IdleTimer.Interval = 11000;
            IdleTimer.Enabled = true;
        }


        //removes ActiveUser from chatroom and prepares object for garbage collection
        public void Destroy()
        {
            IdleTimer.Enabled = false;
            IdleTimer.Stop();
            IdleTimer.Dispose();

            BelongsToHandlesList.Remove(UserHandle);
            BelongsToHandlesList = null;

            BelongsToUsersList.Remove(Id);
            BelongsToUsersList = null;
        }

        //Remember that every 5 seconds, the user marks its IsActive flag as true. 
        //This call happens from the user's JS ajax request when it asks for chatroom information (users list, chatrooms list)
        //This method works by:
        //If a user is marked as active, it marks them as inactive
        //since the user is telling the chatroom they are active every five seconds, then by the next time this method is called (11seconds later), they should be active again
        //if they have not informed the server that they are active, then the user object deletes itself.
        //it does this by removing itself from the chatroom's user list, and marking all its references to null. 
        //This allows garbage collection to delete the object.
        private void IdleCheck(object source, ElapsedEventArgs e)
        {
            if (!IsActive)
            {
                Destroy();
            }
            else
            {
                IsActive = false;
            }
        }
    }
}