using MiniUApi.Models;
using MiniUApi.Models.Database;
using MiniUApi.Models.Enums;
using MiniUApi.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MiniUApi.Data;

namespace MiniUApi.Services
{
    public class VideoService
    {
        private VideoRepository _videoRepository;

        public VideoService()
        {
            _videoRepository = new VideoRepository();
        }
        public QueryResult<MainPageVideoViewModel> GetVideosForMain(int pageNum, OrderType orderBy, OrderDirection order, int pageSize)
        {
            var res = _videoRepository.GetAllVideos(pageNum, orderBy, order, pageSize);
            MainPageVideoViewModel videoRes = new MainPageVideoViewModel();
            int count = 0;
            
            return new QueryResult<MainPageVideoViewModel>(new MainPageVideoViewModel {
                PageNum = pageNum,
                VideosArray = res.Select(e => { count = e.Count; return e as VideoViewModel; }).ToList(),
                PagesAmount = (int)Math.Ceiling(count / (double)pageSize)
        });
        }
        public QueryResult<List<VideoViewModel>> GetVideosForUser(int pageNum, OrderType orderBy, OrderDirection order, Guid userId)
        {
            var res = _videoRepository.GetVideosForUser(pageNum, orderBy, order, userId);
            return new QueryResult<List<VideoViewModel>>(res.ToList());
        }
        public QueryResult<VideoRatingsViewModel> GetVideo(Guid videoId)
        {
            var video = _videoRepository.GetVideoById(videoId);
            var ratings = _videoRepository.GetVideoRatings(videoId).ToList();
            video.Ratings = ratings;
            return new QueryResult<VideoRatingsViewModel>(video);
        }

        public QueryResult<VideoViewModel> SaveVideo(Video inputVideo) {

            var res = _videoRepository.SaveVideo(inputVideo);
            return new QueryResult<VideoViewModel>(res);
        }
        public QueryResult<RatingViewModel> SaveRating(Rating rating)
        {
            var comment = _videoRepository.SaveRating(rating);
            return new QueryResult<RatingViewModel>(comment);
        }
    }
}