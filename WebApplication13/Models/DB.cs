using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace CRUDemo.Models
{
    public class DB
    {
        string connstring = String.Format("Server=vizingest-1;Port=5432;User Id=postgres;Password=45h@dr8X;Database=pilot;");

        // List All Candidates
        public IEnumerable<CandidateInfo> GetAllCandidates()
        {
            List<CandidateInfo> canList = new List<CandidateInfo>();

            using (NpgsqlConnection conn = new NpgsqlConnection(connstring))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * from public.us_election_states order by name";
                cmd.CommandType = CommandType.Text;

                Npgsql.NpgsqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CandidateInfo can = new CandidateInfo();
                    can.ID = Convert.ToInt32(dr["ID"].ToString());
                    can.State = dr["name"].ToString();
                    can.Republicans = Convert.ToInt32(dr["percent_rep"].ToString());
                    can.Democrats = Convert.ToInt32(dr["percent_dem"].ToString());

                    canList.Add(can);
                }

                conn.Close();
            }
            return canList;
        }

        // Create New Candidate
        public void AddCandidate(CandidateInfo can)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connstring))
            {

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT Into public.us_election_states(id,name,percent_rep,percent_dem) values(@ID,@Sname,@Rper,@Dper)";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new NpgsqlParameter("@ID", Convert.ToInt32(can.ID)));
                cmd.Parameters.Add(new NpgsqlParameter("Sname", can.State));
                cmd.Parameters.Add(new NpgsqlParameter("@Rper", can.Republicans));
                cmd.Parameters.Add(new NpgsqlParameter("@Dper", can.Democrats));

                conn.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
            }
        }

        // Update Candidate
        public void UpdateCandidate(CandidateInfo can)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connstring))
            {

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE public.us_election_states SET name=@Sname,percent_rep=@Rper,percent_dem=@Dper where id=@ID";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new NpgsqlParameter("@ID", Convert.ToInt32(can.ID)));
                cmd.Parameters.Add(new NpgsqlParameter("@Sname", can.State));
                cmd.Parameters.Add(new NpgsqlParameter("@Rper", can.Republicans));
                cmd.Parameters.Add(new NpgsqlParameter("@Dper", can.Democrats));

                conn.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
            }
        }

        // Delete Candidate
        public void DeleteCandidate(int? canId)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connstring))
            {

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM public.us_election_states where id=@ID";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new NpgsqlParameter("@ID", Convert.ToInt32(canId)));


                conn.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
            }
        }

        // Get Candidate by ID
        public CandidateInfo GetCandidateById(int? canId)
        {
            CandidateInfo can = new CandidateInfo();

            using (NpgsqlConnection conn = new NpgsqlConnection(connstring))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * from public.us_election_states where id=@ID";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new NpgsqlParameter("ID", canId));
                Npgsql.NpgsqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    can.ID = Convert.ToInt32(dr["ID"].ToString());
                    can.State = dr["name"].ToString();
                    can.Republicans = Convert.ToInt32(dr["percent_rep"].ToString());
                    can.Democrats = Convert.ToInt32(dr["percent_dem"].ToString());

                }

                conn.Close();
            }
            return can;
        }
    }
}
