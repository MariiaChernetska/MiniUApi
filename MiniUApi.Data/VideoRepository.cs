using Dapper;
using MiniUApi.Models.ViewModels;
using MiniUApi.Models.Database;
using MiniUApi.Models.Enums;
using System.Data;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MiniUApi.Data
{
    public class VideoRepository : BaseRepository
    {
        public IEnumerable<CountViewModel> GetAllVideos(int pageNum, OrderType orderBy, OrderDirection order, int pageSize)
        {
            var p = new DynamicParameters();
            p.Add("@PageNum", pageNum, DbType.Int32);
            p.Add("@OrderBy", orderBy, DbType.Int32);
            p.Add("@Order", order, DbType.Int32);
            var res = Connection.Query<CountViewModel>("GetAllVideos", p, commandType: CommandType.StoredProcedure);
            return res;
        }
        public IEnumerable<VideoViewModel> GetVideosForUser(int pageNum, OrderType orderBy, OrderDirection order, Guid userId)
        {
            var p = new DynamicParameters();
            p.Add("@PageNum", pageNum);
            p.Add("@OrderBy", orderBy);
            p.Add("@UserId", userId, DbType.Guid);
            p.Add("@Order", order);
            var res = Connection.Query<VideoViewModel>("GetOfficeVideos", p, commandType: CommandType.StoredProcedure);
            return res;
        }
        public VideoRatingsViewModel GetVideoById(Guid videoId)
        {
            var p = new DynamicParameters();
            p.Add("@VideoId", videoId, DbType.Guid);
            var res = Connection.Query<VideoRatingsViewModel>("GetOneVideo", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return res;
        }
        public IEnumerable<RatingViewModel> GetVideoRatings(Guid videoId)
        {
            var p = new DynamicParameters();
            p.Add("@VideoId", videoId, DbType.Guid);
            var ratings = Connection.Query<RatingViewModel>("GetVideoRatings", p, commandType: CommandType.StoredProcedure);
            return ratings;
        }
        public VideoViewModel SaveVideo(Video inputVideo)
        {
            var p = new DynamicParameters();
            p.Add("@UserId", inputVideo.UserId, DbType.Guid);
            p.Add("@Title", inputVideo.VideoName, DbType.String, size: 50);
            p.Add("@Path", inputVideo.Path, DbType.String);
            p.Add("@ScreenShot", inputVideo.ScreenshotPath, DbType.String);
            p.Add("@Description", inputVideo.Description, DbType.String);
            var res = new VideoViewModel();
            try
            {
                res = Connection.Query<VideoViewModel>("SaveVideo", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch { }
            return res;
        }
        public RatingViewModel SaveRating(Rating rating)
        {
            var p = new DynamicParameters();
            p.Add("@UserId", rating.UserId, DbType.Guid);
            p.Add("@VideoId", rating.VideoId, DbType.Guid);
            p.Add("@Rating", rating.RatingNum, DbType.Int32);
            p.Add("@Comment", rating.Comment, DbType.String);

            var res = Connection.Query<RatingViewModel>("SaveRating", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return res;
        }
    }
}
