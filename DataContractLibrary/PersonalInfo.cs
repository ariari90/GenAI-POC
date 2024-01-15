using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataContractLibrary
{
    [DataContract]
    public class PersonalInfo
    {
        int uniqueId;
        string name;
        string fathersName;
        string mothersName;
        DateTime dateOfBirth;
        string gender;
        string nationality;
        bool isKycDone;
        string address1;
        string address2;
        string city;
        int pinCode;
        string mobile;


        [DataMember]
        public int UniqueId { get => uniqueId; set => uniqueId = value; }

        [DataMember]
        public string Name { get => name; set => name = value; }

        [DataMember]
        public string FathersName { get => fathersName; set => fathersName = value; }

        [DataMember]
        public string MothersName { get => mothersName; set => mothersName = value; }

        [DataMember]
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }

        [DataMember]
        public string Gender { get => gender; set => gender = value; }

        [DataMember]
        public string Nationality { get => nationality; set => nationality = value; }

        [DataMember]
        public bool IsKycDone { get => isKycDone; set => isKycDone = value; }

        [DataMember]
        public string Address1 { get => address1; set => address1 = value; }

        [DataMember]
        public string Address2 { get => address2; set => address2 = value; }

        [DataMember]
        public string City { get => city; set => city = value; }

        [DataMember]
        public int PinCode { get => pinCode; set => pinCode = value; }

        [DataMember]
        public string Mobile { get => mobile; set => mobile = value; }
    }
}
