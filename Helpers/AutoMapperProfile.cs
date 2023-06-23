using AutoMapper;
using JPGStockServer.Entities;
using JPGStockServer.Entities.Notification;
using JPGStockServer.Models.Diode;
using JPGStockServer.Models.Ic;
using JPGStockServer.Models.Lcd;
using JPGStockServer.Models.Module;
using JPGStockServer.Models.Mosfet;
using JPGStockServer.Models.Notification;
using JPGStockServer.Models.Optocoupler;
using JPGStockServer.Models.Others;
using JPGStockServer.Models.Resistor;
using JPGStockServer.Models.Stocks;
using JPGStockServer.Models.Transistor;
using JPGStockServer.Models.Users;


namespace JPGStockServer.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //user//
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<UpdateModel, User>();
            //Stock//
            CreateMap<Stock, StockModels>();
            CreateMap<Stock, LowStockModels>();
            CreateMap<ignoreLowStock, Stock>();
            //Capacitor//
            CreateMap<Stock, CapacitorModels>();
            CreateMap<CapacitorUpdateQuantityModels, Stock>();
            CreateMap<CapacitorUpdateModels, Stock>();
            CreateMap<CapacitorAddModels, Stock>();
            //Diode//
            CreateMap<Stock, DiodeModels>();
            CreateMap<DiodeUpdateQuantityModels, Stock>();
            CreateMap<DiodeUpdateModels, Stock>();
            CreateMap<DiodeAddModels, Stock>();
            //Ic//
            CreateMap<Stock, IcModels>();
            CreateMap<IcUpdateQuantityModels, Stock>();
            CreateMap<IcUpdateModels, Stock>();
            CreateMap<IcAddModels, Stock>();
            //Lcd//
            CreateMap<Stock, LcdModels>();
            CreateMap<LcdUpdateQuantityModels, Stock>();
            CreateMap<LcdUpdateModels, Stock>();
            CreateMap<LcdAddModels, Stock>();
            //Module//
            CreateMap<Stock, ModuleModels>();
            CreateMap<ModuleUpdateQuantityModels, Stock>();
            CreateMap<ModuleUpdateModels, Stock>();
            CreateMap<ModuleAddModels, Stock>();
            //Mosfet//
            CreateMap<Stock, MosfetModels>();
            CreateMap<MosfetUpdateQuantityModels, Stock>();
            CreateMap<MosfetUpdateModels, Stock>();
            CreateMap<MosfetAddModels, Stock>();
            //Optocoupler//
            CreateMap<Stock, OptocouplerModels>();
            CreateMap<OptocouplerUpdateQuantityModels, Stock>();
            CreateMap<OptocouplerUpdateModels, Stock>();
            CreateMap<OptocouplerAddModels, Stock>();
            //Resistor//
            CreateMap<Stock, ResistorModels>();
            CreateMap<ResistorUpdateQuantityModels, Stock>();
            CreateMap<ResistorUpdateModels, Stock>();
            CreateMap<ResistorAddModels, Stock>();
            //Transistor//
            CreateMap<Stock, TransistorModels>();
            CreateMap<TransistorUpdateQuantityModels, Stock>();
            CreateMap<TransistorUpdateModels, Stock>();
            CreateMap<TransistorAddModels, Stock>();
            //Others//
            CreateMap<Stock, OthersModels>();
            CreateMap<OthersUpdateQuantityModels, Stock>();
            CreateMap<OthersUpdateModels, Stock>();
            CreateMap<OthersAddModels, Stock>();
            //Notification//
            CreateMap<NotificationRequest, NotificationRequestModels>();
            CreateMap<NotificationStatusModels, NotificationRequest>();
            CreateMap<NotificationAddModels, NotificationRequest>();

        }
    }
}