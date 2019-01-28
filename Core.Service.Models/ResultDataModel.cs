﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Service.Models
{
    /// <summary>
    /// 请求响应实体
    /// </summary>
    public class ResultDataModel
    {
        /// <summary>
        /// 请求响应实体类
        /// </summary>
        public ResultDataModel()
        {
            Code = 200;
            Message = "操作成功";
        }
        /// <summary>
        /// 响应代码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 响应消息内容
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 返回的响应数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 设置响应状态为成功
        /// </summary>
        /// <param name="message"></param>
        public void SetSuccess(string message = "成功")
        {
            Code = 200;
            Message = message;
        }
        /// <summary>
        /// 设置响应状态为失败
        /// </summary>
        /// <param name="message"></param>
        public void SetFailed(string message = "失败")
        {
            Message = message;
            Code = 999;
        }

        /// <summary>
        /// 设置响应状态为错误
        /// </summary>
        /// <param name="message"></param>
        public void SetError(string message = "错误")
        {
            Code = 500;
            Message = message;
        }

        /// <summary>
        /// 设置响应状态为未知资源
        /// </summary>
        /// <param name="message"></param>
        public void SetNotFound(string message = "未知资源")
        {
            Message = message;
            Code = 404;
        }

        /// <summary>
        /// 设置响应状态为无权限
        /// </summary>
        /// <param name="message"></param>
        public void SetNoPermission(string message = "无权限")
        {
            Message = message;
            Code = 401;
        }

        /// <summary>
        /// 设置响应数据
        /// </summary>
        /// <param name="data"></param>
        public void SetData(object data)
        {
            Data = data;
        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="totalCount"></param>
        public void SetData(object data, int totalCount = 0)
        {
            Data = data;
            TotalCount = totalCount;
        }
    }
}
