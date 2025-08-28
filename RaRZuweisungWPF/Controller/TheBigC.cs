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
        private IFRaRModel model;
        private MainWindow mainWindow;
        public string ErrorMessage { get; set; }
        public TheBigC(MainWindow mw)
        {
            this.model = new RaRModel(this);
            this.mainWindow = mw;
            this.ErrorMessage = "";
        }
        public void AddParticipant(Participant participant)
        {
            try
            {
                model.newParticipant(participant);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
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
                ErrorMessage = ex.ToString();
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

        public void createRounds()
        {

            try
            {
                model.createRounds();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
                Notify();
            }
        }

        public List<Participant> GetParticipants()
        {
            try
            {
                return model.getAllParticipants();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
                Notify();
                return new List<Participant>();
            }
        }

        public List<RaR2> getRaR2Round(int round)
        {
            try
            {
                List<RaR2> rar2s = model.getRaR2Round(round);
                if (rar2s.Count > 0) { return rar2s; }
                throw new Exception("Es wurde noch keine RaR2 Runde erstellt.");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
                Notify();
                return new List<RaR2>();
            }
        }

        public List<RaR3> getRaR3Round(int round)
        {
            try {
                List<RaR3> rar3s = model.getRaR3Round(round);
                if (rar3s.Count == 0) { throw new Exception("Es wurde noch keine RaR3 Runden erstellt."); }
                return rar3s;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
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
                ErrorMessage = ex.ToString();
                Notify();
            }
        }

        public void setRoundPlan(bool[] areRounds2er)
        {
            try
            {
                model.setRoundPlan(areRounds2er);
            } catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
                Notify();
            }
            
        }

        public void textShow(string text) {
            mainWindow.printText(text);
        }
    }
}
