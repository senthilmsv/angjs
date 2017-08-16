using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiAsgRAS.ViewModel;
using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.DAL.Interfaces;

namespace HiAsgRAS.BLL.MappingHelper
{
    public partial class MappingHelper
    {

        public static UserDetailModel MapUserEntityToModel(UserDetail objUserEntity)
        {
            if (objUserEntity != null)
            {
                return new UserDetailModel()
                {
                    Id = objUserEntity.Id,
                    NUID = objUserEntity.NUID + "",
                    UserName = objUserEntity.UserName + "",
                    UserType = objUserEntity.UserType + "",
                    IsActive = objUserEntity.IsActive,
                    EmailID = objUserEntity.EmailID + "",
                    CreatedBy = objUserEntity.CreatedBy,
                    CreatedDate = objUserEntity.CreatedDate,
                    ModifiedBy = objUserEntity.ModifiedBy,
                    ModifiedDate = objUserEntity.ModifiedDate
                };
            }
            else
            {
                return null;

            }
        }


        internal static UserDetail MapUserModelToEntity(UserDetailModel userDetailViewModel)       
        {
            
            return new UserDetail()
            {
                Id = userDetailViewModel.Id,
                NUID = userDetailViewModel.NUID ?? string.Empty,
                UserName = userDetailViewModel.UserName ?? string.Empty,
                UserType = userDetailViewModel.UserType ?? string.Empty,
                IsActive = userDetailViewModel.IsActive,
                EmailID = userDetailViewModel.EmailID ?? string.Empty,
                CreatedBy = userDetailViewModel.CreatedBy ?? string.Empty,
                CreatedDate = userDetailViewModel.CreatedDate,
                ModifiedBy = userDetailViewModel.ModifiedBy ?? string.Empty,
                ModifiedDate = userDetailViewModel.ModifiedDate     
            };
        }


        public static BAOInfoModel MapBAOEnitityToModel(BAOInfo objBAOEntity)
        {
            if (objBAOEntity != null)
            {
                return new BAOInfoModel()
                {
                    Id = objBAOEntity.Id,
                    BAOwnerPrimary = objBAOEntity.BAOwnerPrimary,
                    BAPhonePrimary = objBAOEntity.BAPhonePrimary,
                    BAEmailPrimary = objBAOEntity.BAEmailPrimary,
                    BAOwnerSecondary = objBAOEntity.BAOwnerSecondary,
                    BAPhoneSecondary = objBAOEntity.BAPhoneSecondary,
                    BAEmailSecondary=objBAOEntity.BAEmailSecondary,
                    BAODeptPrimary=objBAOEntity.BAODeptPrimary,
                    BAODeptSecondary=objBAOEntity.BAODeptSecondary,
                    CreatedBy = objBAOEntity.CreatedBy,
                    CreatedDate = objBAOEntity.CreatedDate,
                    ModifiedBy = objBAOEntity.ModifiedBy,
                    ModifiedDate = objBAOEntity.ModifiedDate
                   // IsActive = objBAOEntity.IsActive
                };
            }
            else
            {
                return null;

            }
        }


        internal static BAOInfo MapBAOModelToEntity(BAOInfoModel baoInfoModel)
        {

            return new BAOInfo()
            {
                Id = baoInfoModel.Id,
                BAOwnerPrimary = baoInfoModel.BAOwnerPrimary ?? string.Empty,
                BAPhonePrimary = baoInfoModel.BAPhonePrimary ?? string.Empty,
                BAEmailPrimary = baoInfoModel.BAEmailPrimary ?? string.Empty,
                BAOwnerSecondary = baoInfoModel.BAOwnerSecondary ?? string.Empty,
                BAPhoneSecondary = baoInfoModel.BAPhoneSecondary ?? string.Empty,
                BAEmailSecondary = baoInfoModel.BAEmailSecondary ?? string.Empty,
                BAODeptPrimary = baoInfoModel.BAODeptPrimary ?? string.Empty,
                BAODeptSecondary = baoInfoModel.BAODeptSecondary ?? string.Empty,
                CreatedBy = baoInfoModel.CreatedBy ?? string.Empty,
                CreatedDate = baoInfoModel.CreatedDate,
                ModifiedBy = baoInfoModel.ModifiedBy ?? string.Empty,
                ModifiedDate = baoInfoModel.ModifiedDate
               //  IsActive = baoInfoModel.IsActive
            };
        }

        internal static List<LayerInfoModel> MapLayerTypeEntitiesListToModels(
                        IList<LayerInfo> lstEntityRows)
        {
            List<LayerInfoModel> lstModel = new List<LayerInfoModel>();
            foreach (var objEntity in lstEntityRows)
            {
                lstModel.Add(MapLayerEntityToModel(objEntity));
            }

            return lstModel;
        }


        public static LayerInfoModel MapLayerEntityToModel(LayerInfo objLayerInfo)
        {
            if (objLayerInfo != null)
            {
                return new LayerInfoModel()
                {
                    Id = objLayerInfo.Id,
                    AppLayerName = objLayerInfo.AppLayerName,
                    LayerLocation = objLayerInfo.LayerLocation,
                    CreatedBy = objLayerInfo.CreatedBy,
                    CreatedDate = objLayerInfo.CreatedDate,
                    ModifiedBy = objLayerInfo.ModifiedBy,
                    ModifiedDate = objLayerInfo.ModifiedDate
                  //  IsActive = objLayerInfo.IsActive
                };
            }
            else
            {
                return null;

            }
        }


        internal static LayerInfo MapLayerModelToEntity(LayerInfoModel layerInfoModel)
        {

            return new LayerInfo()
            {
                Id = layerInfoModel.Id,
                AppLayerName = layerInfoModel.AppLayerName ?? string.Empty,
                LayerLocation = layerInfoModel.LayerLocation ?? string.Empty,
                CreatedBy = layerInfoModel.CreatedBy ?? string.Empty,
                CreatedDate = layerInfoModel.CreatedDate,
                ModifiedBy = layerInfoModel.ModifiedBy ?? string.Empty,
                ModifiedDate = layerInfoModel.ModifiedDate
             //   IsActive=layerInfoModel.IsActive

            };
        }

    }
}
