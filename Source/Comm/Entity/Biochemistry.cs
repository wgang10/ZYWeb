using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ZYSoft.Comm.Entity
{
    [DataContract]
    public class Biochemistry
    {
      /// <summary>
      /// ID
      /// </summary>
      [DataMember]
      public virtual int Id { get; set; }        

        /// <summary>
        /// 最終更新日時
        /// </summary>
        [DataMember]
        public virtual string UPDATE_DATETIME { get; set; }        

        /// <summary>
        /// 员工身份证
        /// </summary>
        [DataMember]
        public virtual string EmployeeID{ get; set; }

        /// <summary>
        /// 年月
        /// </summary>
        [DataMember]
        public virtual string YearMonth{ get; set; }

        /// <summary>
        /// 化验号
        /// </summary>
        [DataMember]
        public virtual string HYNo{ get; set; }       

        /// <summary>
        /// 总胆固醇(TC)
        /// </summary>
        [DataMember]
        public virtual float HYTC{ get; set; }

        /// <summary>
        /// 甘油三脂(TG)
        /// </summary>
        [DataMember]
        public virtual float HYTG { get; set; }

        /// <summary>
        /// 高密度脂蛋白胆固醇(HDL-C)
        /// </summary>
        [DataMember]
        public virtual float HYHDLC { get; set; }

        /// <summary>
        /// 低密度脂蛋白胆固醇(LDL-C)
        /// </summary>
        [DataMember]
        public virtual float HYLDLC { get; set; }

        /// <summary>
        /// 载脂蛋白AI(APOAI)
        /// </summary>
        [DataMember]
        public virtual float HYAPOAI { get; set; }

        /// <summary>
        /// 载脂蛋白B(APOB)
        /// </summary>
        [DataMember]
        public virtual float HYAPOB { get; set; }

        /// <summary>
        /// 总胆红素(TBIL)
        /// </summary>
        [DataMember]
        public virtual float HYTBIL { get; set; }

        /// <summary>
        /// 直接胆红素(DBIL)
        /// </summary>
        [DataMember]
        public virtual float HYDBIL { get; set; }

        /// <summary>
        /// 总蛋白(TP)
        /// </summary>
        [DataMember]
        public virtual float HYTP { get; set; }

        /// <summary>
        /// 白蛋白(ALB)
        /// </summary>
        [DataMember]
        public virtual float HYALB { get; set; }

        /// <summary>
        /// 谷丙转氨酶(ALT)=>丙氨酸氨基转移酶
        /// </summary>
        [DataMember]
        public virtual float HYALT { get; set; }

        /// <summary>
        /// 天门冬氨酸氨基转移酶(AST)
        /// </summary>
        [DataMember]
        public virtual float HYAST { get; set; }

        /// <summary>
        /// γ-谷胺酰转氨酶(γ-GT)
        /// </summary>
        [DataMember]
        public virtual float HYGT { get; set; }

        /// <summary>
        /// 碱性磷铵酶(ALP)
        /// </summary>
        [DataMember]
        public virtual float HYALP { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public virtual string HYHBsAg{ get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public virtual string HYHBsAb{ get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public virtual string HYHBeAg{ get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public virtual string HYHBeAb{ get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public virtual string HYHBcAb{ get; set; }

        /// <summary>
        /// 血糖
        /// </summary>
        [DataMember]
        public virtual float HY_GLU { get; set; }

        /// <summary>
        /// 尿素
        /// </summary>
        [DataMember]
        public virtual float HY_UREA { get; set; }

        /// <summary>
        /// 肌酐
        /// </summary>
        [DataMember]
        public virtual float HY_CR { get; set; }

        /// <summary>
        /// 尿酸（UA）
        /// </summary>
        [DataMember]
        public virtual float HYUA { get; set; }

        /// <summary>
        /// 甲胎蛋白
        /// </summary>
        [DataMember]
        public virtual float HY_AFP { get; set; }

        /// <summary>
        /// 癌胚抗原
        /// </summary>
        [DataMember]
        public virtual float HY_CEA { get; set; }
    }
}
