﻿using System;

namespace TeduShop.Model.Abstract
{
    //các cột nhiều  table cùng có, ta đặt interface để dùng chung cho tiện.

    //thêm public cho interface, như thêm cho class
    public interface IAuditable
    {
        //ko  dùng tiền tố cho thuộc tính của interface
        DateTime? CreatedDate { set; get; }

        string CreatedBy { set; get; }

        DateTime? UpdatedDate { set; get; }
        string UpdatedBy { set; get; }

        string MetaKeyword { set; get; }
        string MetaDescription { set; get; }

        bool Status { set; get; }
    }
}