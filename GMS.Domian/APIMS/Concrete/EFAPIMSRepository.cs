using System.Collections.Generic;
using System.Linq;
using GMS.Domian.APIMS.Abstract;
using GMS.Domian.APIMS.Entities;
using GMS.Domian.Infrastraucture;

namespace GMS.Domian.APIMS.Concrete
{
    public class EFAPIMSRepository : IAPIMSRepository
    {
        private EFAPIMSDbContext context = new EFAPIMSDbContext();

        protected List<APIItem> AllAPIItems
        {
            get
            {
                return this.context.APIItems.Include("InputParameters")
                        .Include("Result").ToList();
            }
        }

        protected List<CustomerClass> AllCustomerClasses
        {
            get
            {
                return this.context.CustomerClasses.Include("Properties").ToList();
            }
        }

        public void AddAPIItem(APIItem item)
        {
            this.context.APIItems.Add(item);
            this.context.SaveChanges();
        }

        public InputParameter AddAPIItemInputParam(InputParameter param, int itemID)
        {
            var findItem = this.AllAPIItems.Find(item => item.ID == itemID);
            if (findItem == null)
            {
                return new InputParameter();
            }

            findItem.InputParameters.Add(param);
            this.context.SaveChanges();
            return param;
        }

        public ClassProperty AddClassProperty(ClassProperty classProperty, int customerClassID)
        {
            var findItem = this.AllCustomerClasses.Find(item => item.ID == customerClassID);
            if (findItem == null)
            {
                return new ClassProperty();
            }

            findItem.Properties.Add(classProperty);
            this.context.SaveChanges();
            return classProperty;
        }

        public CustomerClass AddCustomerClass(CustomerClass customerClass, int classificationID)
        {
            customerClass.ClassificationID = classificationID;
            this.context.CustomerClasses.Add(customerClass);
            this.context.SaveChanges();
            return customerClass;
        }

        public void DeleteAPIItemInputParam(InputParameter param)
        {
            var findParam = this.context.InputParameters.Find(param.ID);
            if (findParam == null)
            {
                return;
            }

            this.context.InputParameters.Remove(findParam);
            this.context.SaveChanges();
        }

        public void DeleteClassification(APIClassification classification)
        {
            var find = this.context.APIClassifications.Find(classification.ID);
            if (find == null)
            {
                return;
            }

            this.context.APIClassifications.Remove(find);
            this.context.SaveChanges();
        }

        public void DeleteClassProperty(int id)
        {
            var findParam = this.context.ClassProperties.Find(id);
            if (findParam == null)
            {
                return;
            }

            this.context.ClassProperties.Remove(findParam);
            this.context.SaveChanges();
        }

        public void DeleteCustomerClass(int id)
        {
            var findParam = this.AllCustomerClasses.Find(item => item.ID == id);
            if (findParam == null)
            {
                return;
            }

            var count = findParam.Properties.Count;
            for (var i = 0; i < count; i++)
            {
                this.context.ClassProperties.Remove(findParam.Properties.ToArray()[0]);
            }

            this.context.CustomerClasses.Remove(findParam);
            this.context.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            var find = this.AllAPIItems.Find(item => item.ID == id);
            if (find == null)
            {
                return;
            }

            var count = find.InputParameters.Count;
            for (var i = 0; i < count; i++)
            {
                this.context.InputParameters.Remove(find.InputParameters.ToArray()[0]);
            }

            this.context.Results.Remove(find.Result);
            this.context.APIItems.Remove(find);
            this.context.SaveChanges();
        }

        public void DeleteType(APIType type)
        {
            var find = this.context.APITypes.Find(type.ID);
            if (find == null)
            {
                return;
            }

            this.context.APITypes.Remove(find);
            this.context.SaveChanges();
        }

        public IEnumerable<APIClassification> GetAllAPIClassifications()
        {
            return this.context.APIClassifications;
        }

        public IEnumerable<APIItem> GetAllAPIItemByClassificationID(int id)
        {
            return this.AllAPIItems.Where(item => item.ClassificationID == id);
        }

        public IEnumerable<CustomerClass> GetAllBaseCustomerClasses()
        {
            return this.AllCustomerClasses.Where(item => item.IsCommon == 0);
        }

        public IEnumerable<CustomerClass> GetAllCustomerClassesByClassificationID(int id, bool relevant)
        {
            if (relevant)
            {
                return this.AllCustomerClasses.Where(item => item.ClassificationID == id
                    || item.IsCommon == 1);
            }
            else
            {
                return this.AllCustomerClasses.Where(item => item.ClassificationID == id && item.IsCommon == 0);
            }
        }

        public APIType GetAPITypeByID(int id)
        {
            return this.context.APITypes.Find(id);
        }

        public IEnumerable<APIType> GetAPITypes()
        {
            return this.context.APITypes;
        }

        public APIClassification GetClassificationByID(int id)
        {
            return this.context.APIClassifications.Find(id);
        }

        public CustomerClass GetClassTypeByID(int id)
        {
            return this.AllCustomerClasses.Find(item => item.ID == id);
        }

        public IEnumerable<CustomerClass> GetCommonClassTypes()
        {
            return this.AllCustomerClasses.Where(item => item.IsCommon == 1);
        }

        public CustomerClass GetCustomerClassByID(int id)
        {
            return this.AllCustomerClasses.Find(c => c.ID == id);
        }

        public APIItem GetItemByID(int id)
        {
            return this.AllAPIItems.Find(i => i.ID == id);
        }

        public IEnumerable<APIClassification> GetVisiableAPIClassifications()
        {
            return this.context.APIClassifications.Where(item => item.Visiable == true);
        }

        public IEnumerable<APIItem> GetVisiableAPIItemByClassificationID(int id)
        {
            return this.AllAPIItems.Where(item => item.ClassificationID == id && item.Visiable == 1);
        }

        public void SaveAPIItemInputParam(InputParameter param)
        {
            var findParam = this.context.InputParameters.Find(param.ID);
            if (findParam == null)
            {
                return;
            }

            findParam.DeepClone(param);
            this.context.SaveChanges();
        }

        public void SaveAPIItemResult(Result result)
        {
            var findItem = this.context.Results.Find(result.ID);
            if (findItem == null)
            {
                return;
            }

            findItem.DeepClone(result);
            this.context.SaveChanges();
        }

        public void SaveClassification(APIClassification classification)
        {
            if (classification.ID == 0)
            {
                this.context.APIClassifications.Add(classification);
            }
            else
            {
                var find = this.context.APIClassifications.Find(classification.ID);
                if (find != null)
                {
                    find.DeepClone(classification);
                }
            }

            this.context.SaveChanges();
        }

        public void SaveClassProperty(ClassProperty classProperty)
        {
            var find = this.context.ClassProperties.Find(classProperty.ID);
            if (find != null)
            {
                find.DeepClone(classProperty);
            }

            this.context.SaveChanges();
        }

        public void SaveCustomerClass(CustomerClass customerClass)
        {
            var find = this.AllCustomerClasses.Find(item => item.ID == customerClass.ID);
            if (find != null)
            {
                find.DeepClone(customerClass);
            }

            this.context.SaveChanges();
        }

        public void SaveItemBase(APIItemBase item)
        {
            var find = this.GetItemByID(item.ID);
            if (find != null)
            {
                find.DeepClone(item);
            }

            this.context.SaveChanges();
        }

        public void SaveType(APIType type)
        {
            if (type.ID == 0)
            {
                this.context.APITypes.Add(type);
            }
            else
            {
                var find = this.context.APITypes.Find(type.ID);
                if (find != null)
                {
                    find.DeepClone(type);
                }
            }

            this.context.SaveChanges();
        }

        public void SetCommonCustomerClass(int id)
        {
            this.context.CustomerClasses.Find(id).IsCommon = 1;
            this.context.SaveChanges();
        }
    }
}
