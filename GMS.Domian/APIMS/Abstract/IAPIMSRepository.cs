using GMS.Domian.APIMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domian.APIMS.Abstract
{
    public interface IAPIMSRepository
    {
        IEnumerable<APIType> GetAPITypes();
        APIType GetAPITypeByID(int id);
        IEnumerable<APIClassification> GetAllAPIClassifications();
        IEnumerable<APIClassification> GetVisiableAPIClassifications();
        APIClassification GetClassificationByID(int id);
        IEnumerable<APIItem> GetAllAPIItemByClassificationID(int id);
        IEnumerable<APIItem> GetVisiableAPIItemByClassificationID(int id);
        APIItem GetItemByID(int id);
        IEnumerable<CustomerClass> GetAllCustomerClassesByClassificationID(int id,bool relevant);
        IEnumerable<CustomerClass> GetAllBaseCustomerClasses();
        IEnumerable<CustomerClass> GetCommonClassTypes();
        CustomerClass GetClassTypeByID(int id);
        CustomerClass GetCustomerClassByID(int id);


        void AddAPIItem(APIItem item);
        InputParameter AddAPIItemInputParam(InputParameter param, int ItemID);
        CustomerClass AddCustomerClass(CustomerClass customerClass, int classificationID);
        ClassProperty AddClassProperty(ClassProperty classProperty,int customerClassID);

        void SaveType(APIType type);
        void SaveClassification(APIClassification classification);
        void SaveItemBase(APIItemBase item);
        void SaveAPIItemInputParam(InputParameter param);
        void SaveAPIItemResult(Result result);
        void SaveCustomerClass(CustomerClass customerClass);
        void SaveClassProperty(ClassProperty classProperty);

        void SetCommonCustomerClass(int id);

        void DeleteType(APIType type);
        void DeleteClassification(APIClassification classification);
        void DeleteItem(int id);
        void DeleteAPIItemInputParam(InputParameter param);
        void DeleteCustomerClass(int id);
        void DeleteClassProperty(int id);
    }
}
