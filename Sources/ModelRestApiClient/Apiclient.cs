using AutoMapper;
using Model;
using Newtonsoft.Json;
using SendGrid.Helpers.Errors.Model;
using Shared;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TarotDB;

namespace ModelRestApiClient
{
    public class ApiClient<TModel,TEntity> : IDataManager<TModel>
    {    
        private readonly string _apiUrl = null; // URL API
        private readonly IMapper _mapper;

        public async Task<bool> AddRange(params TModel[] items)
        {
            bool result = true;
            foreach (var item in items)
            {
                result &= await Insert(item) != null;
            }

            return result;
        }

        public async Task Clear()
        {
            using (var client = new HttpClient())
            {
                var result = await client.DeleteAsync($"{_apiUrl}").ConfigureAwait(false);
                if (!result.IsSuccessStatusCode)
                {
                    throw new BadRequestException(result.ReasonPhrase);
                }
            }
        }

        public async Task<int> Count()
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync($"{_apiUrl}/count").ConfigureAwait(false);
                if (!result.IsSuccessStatusCode)
                {
                    throw new BadRequestException(result.ReasonPhrase);
                }

                var value = JsonConvert.DeserializeObject<int>(await result.Content.ReadAsStringAsync());
                return value;
            }
        }

        public async Task Delete(object id)
        {
            using (var client = new HttpClient())
            {
                var result = await client.DeleteAsync($"{_apiUrl}/{id}").ConfigureAwait(false);
                if (!result.IsSuccessStatusCode)
                {
                    throw new BadRequestException(result.ReasonPhrase);
                }
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<TModel> FindById(object id)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync($"{_apiUrl}/{id}").ConfigureAwait(false);
                if (!result.IsSuccessStatusCode)
                {
                    throw new BadRequestException(result.ReasonPhrase);
                }

                var dto = JsonConvert.DeserializeObject<TEntity>(await result.Content.ReadAsStringAsync());
                return _mapper.Map<TModel>(dto);
            }
        }

        public async Task<IEnumerable<TModel>> GetItems(int index, int count)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync($"{_apiUrl}/?index={index}&count={count}").ConfigureAwait(false);
                if (!result.IsSuccessStatusCode)
                {
                    throw new BadRequestException(result.ReasonPhrase);
                }

                var dto = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(
                    await result.Content.ReadAsStringAsync());
                return _mapper.Map<IEnumerable<TModel>>(dto);
            }
        }

        public async Task<TModel> Insert(TModel item)
        {
            using (var client = new HttpClient())
            {
                var dto = _mapper.Map<TEntity>(item);

                var json = JsonConvert.SerializeObject(dto);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var result = await client.PostAsync($"{_apiUrl}", httpContent).ConfigureAwait(false);
                if (!result.IsSuccessStatusCode)
                {
                    throw new BadRequestException(result.ReasonPhrase);
                }

                var resultDto = JsonConvert.DeserializeObject<TEntity>(await result.Content.ReadAsStringAsync());
                return _mapper.Map<TModel>(resultDto);
            }
        }

        public async Task<TModel> Update(object id, TModel item)
        {
            using (var client = new HttpClient())
            {
                var dto = _mapper.Map<TEntity>(item);

                var json = JsonConvert.SerializeObject(dto);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var result = await client.PutAsync($"{_apiUrl}/{id}", httpContent).ConfigureAwait(false);
                if (!result.IsSuccessStatusCode)
                {
                    throw new BadRequestException(result.ReasonPhrase);
                }

                var resultDto = JsonConvert.DeserializeObject<TEntity>(await result.Content.ReadAsStringAsync());
                return _mapper.Map<TModel>(resultDto);
            }
        }
    }
}
