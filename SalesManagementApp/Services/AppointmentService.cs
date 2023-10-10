using SalesManagementApp.Data;
using SalesManagementApp.Entities;
using SalesManagementApp.Extensions;
using SalesManagementApp.Models;
using SalesManagementApp.Services.Contracts;

namespace SalesManagementApp.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly SalesManagementDbContext salesManagementDbContext;

        public AppointmentService(SalesManagementDbContext salesManagementDbContext)
        {
            this.salesManagementDbContext = salesManagementDbContext;
        }
        public Task AddAppointment(AppointmentModel appointmentModel)
        {
            try
            {
                Appointment appointment = appointmentModel.Convert();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task DeleteAppointment(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AppointmentModel>> GetAppointments()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAppointment(AppointmentModel appointmentModel)
        {
            throw new NotImplementedException();
        }
    }
}
