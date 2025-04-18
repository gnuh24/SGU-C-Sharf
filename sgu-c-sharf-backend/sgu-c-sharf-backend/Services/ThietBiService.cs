using System;
using System.Collections.Generic;
using sgu_c_sharf_backend.Models.ThietBi;
using sgu_c_sharf_backend.Repositories;

namespace sgu_c_sharf_backend.Services
{
    public class ThietBiService
    {
        private readonly ThietBiRepository _thietBiRepository;

        public ThietBiService(ThietBiRepository thietBiRepository)
        {
            _thietBiRepository = thietBiRepository;
        }

        public ThietBiDetailDTO GetById(int id)
        {
            var thietBi = _thietBiRepository.GetById(id);
            if (thietBi == null)
                throw new Exception("Thiết bị không tồn tại.");
            return thietBi;
        }

        public IEnumerable<ThietBiListDTO> GetAll()
        {
            return _thietBiRepository.GetAll();
        }

        public void Add(ThietBiCreateForm form)
        {
            _thietBiRepository.Add(form);
        }

        public void Update(int id, ThietBiUpdateForm form)
        {
            _thietBiRepository.Update(id, form);
        }

        public void Delete(int id)
        {
            _thietBiRepository.Delete(id);
        }

        public IEnumerable<ThietBiListDTO> Search(string tenThietBi, int? idLoaiThietBi)
        {
            return _thietBiRepository.Search(tenThietBi, idLoaiThietBi);
        }
    }
}