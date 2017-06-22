using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ClientWebService;

namespace BikeBook.Views.CustomUIElements
{
    public class ProfileNameCell : GearCell
    {
        public ProfileNameCell(User user) : base()
        {
            ImageSerializer imageSerializer = new ImageSerializer();
            ImageSource = imageSerializer.GetProfilePicture(user.email);
            Headline = user.name;
        }
    }
}
