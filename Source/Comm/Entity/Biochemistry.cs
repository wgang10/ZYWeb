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
        /// 数据作成日時
        /// </summary>
        [DataMember]
        public virtual string CREATE_DATETIME{ get; set; }

        /// <summary>
        /// 最終更新日時
        /// </summary>
        [DataMember]
        public virtual string UPDATE_DATETIME{ get; set; }

        /// <summary>
        /// 最終処理区分
        /// </summary>
        [DataMember]
        public virtual string TRANS_STATE{ get; set; }

        /// <summary>
        /// 更新担当者ID
        /// </summary>
        [DataMember]
        public virtual string UPDATER_ID{ get; set; }

        /// <summary>
        /// 更新端末
        /// </summary>
        [DataMember]
        public virtual string TERMINAL_CD{ get; set; }

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
        /// 医师
        /// </summary>
        [DataMember]
        public virtual string HYDr{ get; set; }

        /// <summary>
        /// 总胆固醇(TC)
        /// </summary>
        [DataMember]
        public virtual string HYTC{ get; set; }

        /// <summary>
        /// 甘油三脂(TG)
        /// </summary>
        [DataMember]
        public virtual string HYTG{ get; set; }

        /// <summary>
        /// 高密度脂蛋白胆固醇(HDL-C)
        /// </summary>
        [DataMember]
        public virtual string HYHDLC{ get; set; }

        /// <summary>
        /// 低密度脂蛋白胆固醇(LDL-C)
        /// </summary>
        [DataMember]
        public virtual string HYLDLC{ get; set; }

        /// <summary>
        /// 载脂蛋白AI(APOAI)
        /// </summary>
        [DataMember]
        public virtual string HYAPOAI{ get; set; }

        /// <summary>
        /// 载脂蛋白B(APOB)
        /// </summary>
        [DataMember]
        public virtual string HYAPOB{ get; set; }

        /// <summary>
        /// 总胆红素(TBIL)
        /// </summary>
        [DataMember]
        public virtual string HYTBIL{ get; set; }

        /// <summary>
        /// 直接胆红素(DBIL)
        /// </summary>
        [DataMember]
        public virtual string HYDBIL{ get; set; }

        /// <summary>
        /// 总蛋白(TP)
        /// </summary>
        [DataMember]
        public virtual string HYTP{ get; set; }

        /// <summary>
        /// 白蛋白(ALB)
        /// </summary>
        [DataMember]
        public virtual string HYALB{ get; set; }

        /// <summary>
        /// 谷丙转氨酶(ALT)=>丙氨酸氨基转移酶
        /// </summary>
        [DataMember]
        public virtual string HYALT{ get; set; }

        /// <summary>
        /// 天门冬氨酸氨基转移酶(AST)
        /// </summary>
        [DataMember]
        public virtual string HYAST{ get; set; }

        /// <summary>
        /// γ-谷胺酰转氨酶(γ-GT)
        /// </summary>
        [DataMember]
        public virtual string HYGT{ get; set; }

        /// <summary>
        /// 碱性磷铵酶(ALP)
        /// </summary>
        [DataMember]
        public virtual string HYALP{ get; set; }

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
        public virtual string HY_GLU{ get; set; }

        /// <summary>
        /// 尿素
        /// </summary>
        [DataMember]
        public virtual string HY_UREA{ get; set; }

        /// <summary>
        /// 肌酐
        /// </summary>
        [DataMember]
        public virtual string HY_CR{ get; set; }

        /// <summary>
        /// 尿酸（UA）
        /// </summary>
        [DataMember]
        public virtual string HYUA{ get; set; }

        /// <summary>
        /// 甲胎蛋白
        /// </summary>
        [DataMember]
        public virtual string HY_AFP{ get; set; }

        /// <summary>
        /// 癌胚抗原
        /// </summary>
        [DataMember]
        public virtual string HY_CEA{ get; set; }
    }
}
