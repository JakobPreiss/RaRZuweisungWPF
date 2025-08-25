using RaRZuweisungWPF.aView;
using RaRZuweisungWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RaRZuweisungWPF.Controller
{
    public class TheBigC : IFController
    {
        private IFRaRModel model = new RaRModel();
        private MainWindow mainWindow;
        public string ErrorMessage { get; set; }
        public TheBigC(MainWindow mw)
        {
            this.model = new RaRModel();
            this.mainWindow = mw;
        }
        public void AddParticipant(Participant participant)
        {
            try
            {
                model.newParticipant(participant);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Notify();
            }
        }
        public void Notify()
        {
            mainWindow.displayErrorMessage(ErrorMessage);
        }

        public bool changePairing(int round, Participant oldRoundParticipant, string name1, string name2, string name3)
        {
            throw new NotImplementedException();
        }

        public bool changePairing(int round, Participant oldRoundParticipant, string name1, string name2)
        {
            throw new NotImplementedException();
        }

        public void changeParticipant(Participant participant, string name, bool old, Dictionary<int, bool> availability)
        {
            try
            {
                model.changeParticipant(participant, name, old, availability);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Notify();
            } 
        }

        public bool checkIfRoundIs2(int round)
        {
            
            if(model.getRaR2Round(round).Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void createRound(int round, bool isRaR2Rund)
        {
            model.createNextRound(round, isRaR2Rund);
        }

        public List<Participant> GetParticipants()
        {
            try
            {
                return model.getAllParticipants();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Notify();
                return new List<Participant>();
            }
        }

        public List<RaR2> getRaR2Round(int round)
        {
            try
            {
                if (model.getRaR2Round(round).Count == 0)
                {
                    throw new Exception("Es wurden noch keine RaR2 Runden erstellt.");
                }
                return model.getRaR2Round(round);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Notify();
                return new List<RaR2>();
            }
        }

        public List<RaR3> getRaR3Round(int round)
        {
            try { 
                if (model.getRaR3Round(round).Count == 0)
                {
                    throw new Exception("Es wurden noch keine RaR3 Runden erstellt.");
                }
                return model.getRaR3Round(round);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Notify();
                return new List<RaR3>();
            }
        }

        public void RemoveParticipant(Participant participant)
        {
            try { 
                model.deleteParticipant(participant);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
                Notify();
            }
        }

        public void resetDatabase()
        {
            try { 
                model.resetDatabase();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Notify();
            }
        }
    }
}
