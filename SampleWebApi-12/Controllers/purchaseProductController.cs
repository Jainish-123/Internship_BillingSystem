using classADO_12;
using SampleWebApi_12.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SampleWebApi_12.Controllers
{
    public class purchaseProductController : ApiController
    {
        productEntities1 db = new productEntities1();

        purchase purObj = new purchase();

        double total_amount = 0;

        [Route("api/purchaseProduct/insertData")]
        [HttpPost]
        public IHttpActionResult insertData([FromBody] purchase_product[] req)
        {
            test obj = new test();

            for (int i = 0; i < req.Length; i++)
            {
                total_amount += req[i].amount;
            }

            purObj.pur_no = obj.getUniqueNumber();
            purObj.date = DateTime.Now;
            purObj.total_amount = total_amount;

            db.purchases.Add(purObj);

            db.SaveChanges();

            int purId = purObj.pur_id;

            for (int i = 0; i < req.Length; i++)
            {
                req[i].pur_id = purId;
                db.purchase_product.Add(req[i]);
            }

            db.SaveChanges();

            return Ok();
        }


        [Route("api/purchaseProduct/deleteDataInBoth")]
        [HttpDelete]
        public IHttpActionResult deleteDataInBoth([FromBody] int id)
        {
            var obj1 = db.purchases.Where(alias => alias.pur_id == id).FirstOrDefault();

            db.Entry(obj1).State = System.Data.Entity.EntityState.Deleted;

            db.purchase_product.RemoveRange(db.purchase_product.Where(alias => alias.pur_id == id));

            db.SaveChanges();

            return Ok();
        }

        [Route("api/purchaseProduct/deleteDataInPurchase_Product")]
        [HttpDelete]
        public IHttpActionResult deleteDataInPurchase_Product([FromBody] int id)
        {
            var obj1 = db.purchase_product.Where(alias => alias.pur_pro_id == id).FirstOrDefault();

            double tmpAmount = obj1.amount;

            db.Entry(obj1).State = System.Data.Entity.EntityState.Deleted;

            var obj2 = db.purchases.Where(alias => alias.pur_id == obj1.pur_id).FirstOrDefault();

            double tmpTotalAmount = obj2.total_amount;

            tmpTotalAmount = tmpTotalAmount - tmpAmount;

            obj2.total_amount = tmpTotalAmount;

            db.SaveChanges();

            return Ok();
        }

        [Route("api/purchaseProduct/updateData")]
        [HttpPut]
        public IHttpActionResult updateData(purchase_product req)
        {
            var obj1 = db.purchase_product.Where(alias => alias.pur_pro_id == req.pur_pro_id).FirstOrDefault();

            obj1.qty = req.qty;

            double tmpAmount = obj1.amount - req.amount;

            obj1.amount = req.amount;

            var obj2 = db.purchases.Where(alias => alias.pur_id == obj1.pur_id).FirstOrDefault();

            double tmpTotalAmount = obj2.total_amount;

            tmpTotalAmount = tmpTotalAmount - tmpAmount;

            obj2.total_amount = tmpTotalAmount;

            db.SaveChanges();

            return Ok();
        }

    }
}
