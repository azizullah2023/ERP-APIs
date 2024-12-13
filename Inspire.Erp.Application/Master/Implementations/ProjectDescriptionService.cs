using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public class ProjectDescriptionService : IProjectDescriptionService
    {
        private IRepository<ProjectDescriptionMaster> ProjectDescriptionrepository;
        public ProjectDescriptionService(IRepository<ProjectDescriptionMaster> _ProjectDescriptionrepository)
        {
            ProjectDescriptionrepository = _ProjectDescriptionrepository;
        }
        public IEnumerable<ProjectDescriptionMaster> InsertProjectDesc(ProjectDescriptionMaster projectDescriptionMaster)
        {
            bool valid = false;
            try
            {
                valid = true;
                ////int mxc = 0;
                ////mxc = (int)ProjectDescriptionrepository.GetAsQueryable().Where(k => k.ProjectDescriptionMasterProjectDescriptionId != null).Select(x => x.ProjectDescriptionMasterProjectDescriptionId).Max();
                ////if (mxc == null) { mxc = 1; } else { mxc = mxc + 1; }

                int mxc = Convert.ToInt32(ProjectDescriptionrepository.GetAsQueryable()
                                                        .Where(x => x.ProjectDescriptionMasterProjectDescriptionId > 0)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.ProjectDescriptionMasterProjectDescriptionId)) + 1;


                projectDescriptionMaster.ProjectDescriptionMasterProjectDescriptionId = mxc;
                ProjectDescriptionrepository.Insert(projectDescriptionMaster);
            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return this.GetAllProjectDesc();
        }
        public IEnumerable<ProjectDescriptionMaster> UpdateProjectDesc(ProjectDescriptionMaster projectDescriptionMaster)
        {
            bool valid = false;
            try
            {
                ProjectDescriptionrepository.Update(projectDescriptionMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return this.GetAllProjectDesc();
        }
        public IEnumerable<ProjectDescriptionMaster> DeleteProjectDesc(ProjectDescriptionMaster projectDescriptionMaster)
        {
            bool valid = false;
            try
            {
                ProjectDescriptionrepository.Delete(projectDescriptionMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return this.GetAllProjectDesc();
        }

        public IEnumerable<ProjectDescriptionMaster> GetAllProjectDesc()
        {
            IEnumerable<ProjectDescriptionMaster> projectDescriptionMaster;
            try
            {
                projectDescriptionMaster = ProjectDescriptionrepository.GetAll();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return projectDescriptionMaster;
        }

        public IEnumerable<ProjectDescriptionMaster> GetAllProjectDescById(int id)
        {
            IEnumerable<ProjectDescriptionMaster> projectDescriptionMaster;
            try
            {
                projectDescriptionMaster = ProjectDescriptionrepository.GetAsQueryable().Where(k => k.ProjectDescriptionMasterProjectDescriptionId == id).Select(k => k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return projectDescriptionMaster;

        }

    }
}
