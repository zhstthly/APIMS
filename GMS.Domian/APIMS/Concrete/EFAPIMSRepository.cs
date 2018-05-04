using GMS.Domian.APIMS.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMS.Domian.APIMS.Entities;
using GMS.Domian.Infrastraucture;
using System.Data.Entity;

namespace GMS.Domian.APIMS.Concrete
{
    public class EFAPIMSRepository : IAPIMSRepository
    {
        private EFAPIMSDbContext context = new EFAPIMSDbContext();

        protected List<APIItem> AllAPIItems
        {
            get
            {
                return context.APIItems.Include("InputParameters")
                        .Include("Result").ToList();
            }
        }

        protected List<CustomerClass> AllCustomerClasses
        {
            get
            {
                return context.CustomerClasses.Include("Properties").ToList();
            }
        }


        public void AddAPIItem(APIItem item)
        {
            context.APIItems.Add(item);
            context.SaveChanges();
        }

        public InputParameter AddAPIItemInputParam(InputParameter param, int ItemID)
        {
            var findItem = AllAPIItems.Find(item=>item.ID == ItemID);
            if (findItem == null)
                return new InputParameter();
            findItem.InputParameters.Add(param);
            context.SaveChanges();
            return param;
        }

        public ClassProperty AddClassProperty(ClassProperty classProperty, int customerClassID)
        {
            var findItem = AllCustomerClasses.Find(item=>item.ID == customerClassID);
            if (findItem == null)
                return new ClassProperty();
            findItem.Properties.Add(classProperty);
            context.SaveChanges();
            return classProperty;
        }

        public CustomerClass AddCustomerClass(CustomerClass customerClass, int classificationID)
        {
            customerClass.ClassificationID = classificationID;
            context.CustomerClasses.Add(customerClass);
            context.SaveChanges();
            return customerClass;
        }

        public void DeleteAPIItemInputParam(InputParameter param)
        {
            var findParam = context.InputParameters.Find(param.ID);
            if (findParam == null)
                return;
            context.InputParameters.Remove(findParam);
            context.SaveChanges();
        }

        public void DeleteClassification(APIClassification classification)
        {
            var find = context.APIClassifications.Find(classification.ID);
            if (find == null)
                return;
            context.APIClassifications.Remove(find);
            context.SaveChanges();
        }

        public void DeleteClassProperty(int id)
        {
            var findParam = context.ClassProperties.Find(id);
            if (findParam == null)
                return;
            context.ClassProperties.Remove(findParam);
            context.SaveChanges();
        }

        public void DeleteCustomerClass(int id)
        {
            var findParam = AllCustomerClasses.Find(item =>item.ID == id);
            if (findParam == null)
                return;
            var count = findParam.Properties.Count;
            for (var i = 0; i < count; i++)
                context.ClassProperties.Remove(findParam.Properties.ToArray()[0]);
            context.CustomerClasses.Remove(findParam);
            context.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            var find = AllAPIItems.Find(item => item.ID == id);
            if (find == null)
                return;
            var count = find.InputParameters.Count;
            for (var i = 0; i < count; i++)
                context.InputParameters.Remove(find.InputParameters.ToArray()[0]);
            context.Results.Remove(find.Result);
            context.APIItems.Remove(find);
            context.SaveChanges();
        }

        public void DeleteType(APIType type)
        {
            var find = context.APITypes.Find(type.ID);
            if (find == null)
                return;
            context.APITypes.Remove(find);
            context.SaveChanges();
        }

        public IEnumerable<APIClassification> GetAllAPIClassifications()
        {
            return context.APIClassifications;
        }

        public IEnumerable<APIItem> GetAllAPIItemByClassificationID(int id)
        {
            return AllAPIItems.Where(item => item.ClassificationID == id);
        }

        public IEnumerable<CustomerClass> GetAllBaseCustomerClasses()
        {
            return AllCustomerClasses.Where(item => item.IsCommon == 0);
        }

        public IEnumerable<CustomerClass> GetAllCustomerClassesByClassificationID(int id, bool relevant)
        {
            if (relevant)
                return AllCustomerClasses.Where(item => item.ClassificationID == id
                    || item.IsCommon == 1);
            else
                return AllCustomerClasses.Where(item => item.ClassificationID == id && item.IsCommon == 0);
        }

        public APIType GetAPITypeByID(int id)
        {
            return context.APITypes.Find(id);
        }

        public IEnumerable<APIType> GetAPITypes()
        {
            return context.APITypes;
        }

        public APIClassification GetClassificationByID(int id)
        {
            return context.APIClassifications.Find(id);
        }

        public CustomerClass GetClassTypeByID(int id)
        {
            return AllCustomerClasses.Find(item => item.ID == id);
        }

        public IEnumerable<CustomerClass> GetCommonClassTypes()
        {
            return AllCustomerClasses.Where(item=>item.IsCommon == 1);
        }

        public CustomerClass GetCustomerClassByID(int id)
        {
            return AllCustomerClasses.Find(c => c.ID == id);
        }

        public APIItem GetItemByID(int id)
        {
            return AllAPIItems.Find(i=>i.ID == id);
        }

        public IEnumerable<APIClassification> GetVisiableAPIClassifications()
        {
            return context.APIClassifications.Where(item => item.Visiable == true);
        }

        public IEnumerable<APIItem> GetVisiableAPIItemByClassificationID(int id)
        {
            return AllAPIItems.Where(item => item.ClassificationID == id && item.Visiable == 1);
        }

        public void SaveAPIItemInputParam(InputParameter param)
        {
            var findParam = context.InputParameters.Find(param.ID);
            if (findParam == null)
                return;
            findParam.DeepClone(param);
            context.SaveChanges();
        }

        public void SaveAPIItemResult(Result result)
        {
            var findItem = context.Results.Find(result.ID);
            if (findItem == null)
                return;
            findItem.DeepClone(result);
            context.SaveChanges();
        }

        public void SaveClassification(APIClassification classification)
        {
            if(classification.ID == 0)
                context.APIClassifications.Add(classification);
            else
            {
                var find = context.APIClassifications.Find(classification.ID);
                if(find != null)
                {
                    find.DeepClone(classification);
                }
            }
            context.SaveChanges();
        }

        public void SaveClassProperty(ClassProperty classProperty)
        {
            var find = context.ClassProperties.Find(classProperty.ID);
            if (find != null)
            {
                find.DeepClone(classProperty);
            }
            context.SaveChanges();
        }

        public void SaveCustomerClass(CustomerClass customerClass)
        {
            var find = AllCustomerClasses.Find(item =>item.ID == customerClass.ID);
            if (find != null)
            {
                find.DeepClone(customerClass);
            }
            context.SaveChanges();
        }

        public void SaveItemBase(APIItemBase item)
        {
            var find = GetItemByID(item.ID);
            if (find != null)
            {
                find.DeepClone(item);
            }
            context.SaveChanges();
        }

        public void SaveType(APIType type)
        {
            if (type.ID == 0)
                context.APITypes.Add(type);
            else
            {
                var find = context.APITypes.Find(type.ID);
                if (find != null)
                {
                    find.DeepClone(type);
                }
            }
            context.SaveChanges();
        }

        public void SetCommonCustomerClass(int id)
        {
            context.CustomerClasses.Find(id).IsCommon = 1;
            context.SaveChanges();
        }
    }
}
