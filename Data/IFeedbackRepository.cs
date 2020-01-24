using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessBackend.Models;

namespace ChessBackend.Data
{
    public interface IFeedbackRepository
    {
        Task Add(Feedback feedback);
        Task<IEnumerable<Feedback>> GetFeedbacks();
    }
}
