using System.Linq.Expressions;
using System.Windows;
using RaRZuweisungWPF.Controller;
using RaRZuweisungWPF.Model;

namespace RaRTests
{
    public class basicRaRTests
    {
        [Fact]
        public void CreateRoundsWorksWith12OldAnd10NewAndRound1And2As2er()
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            Assignment assignment = new Assignment(dataBaseAccess);
            RaRModel raRModel = new RaRModel(assignment, dataBaseAccess);
            TheBigC controller = new TheBigC(raRModel);
            raRModel.controller = controller;

            controller.resetDatabase();

            var testParticipants = new List<Participant>
            {
                new Participant("Hannah", true, true, true, true, true, true),
                new Participant("Elli", true, true, true, true, true, true),
                new Participant("Paul", true, true, true, true, true, true),
                new Participant("Jake", true, true, true, true, true, true),
                new Participant("Petra", true, true, true, true, true, true),
                new Participant("Laurin", true, true, true, true, true, true),
                new Participant("Gregor", true, true, true, true, true, true),
                new Participant("Sina", true, true, true, true, true, true),
                new Participant("Johanna", true, true, true, true, true, true),
                new Participant("Gloria", true, true, true, true, true, true),
                new Participant("Fabienne", true, true, true, true, true, true),
                new Participant("Eline", true, true, true, true, true, true),

                new Participant("Konstantin", false, true, true, true, true, true),
                new Participant("Kirsten", false, true, true, true, true, true),
                new Participant("Janis", false, true, true, true, true, true),
                new Participant("Alex", false, true, true, true, true, true),
                new Participant("Pascal", false, true, true, true, true, true),
                new Participant("Phillipe", false, true, true, true, true, true),
                new Participant("Ari", false, true, true, true, true, true),
                new Participant("Paulina", false, true, true, true, true, true),
                new Participant("jana", false, true, true, true, true, true),
                new Participant("Anja", false, true, true, true, true, true),
                //new Participant("Klaus", false, true, true, true, true, true),
                //new Participant("Viktor", false, true, true, true, true, true)
            };

            foreach (Participant participant in testParticipants)
            {
                controller.AddParticipant(participant);
            }
            controller.setRoundPlan(new bool[] { true, false, false, true, false });
            controller.createRounds();
            Assert.True(controller.getRaR2Round(1).Count == 10);
            Assert.True(controller.getRaR2Round(4).Count == 10);
            Assert.True(controller.getRaR3Round(2).Count == 7);
            Assert.True(controller.getRaR3Round(3).Count == 7);
            Assert.True(controller.getRaR3Round(5).Count == 7);
            for (int i = 1; i <= 5; i++)
            {
                Assert.True(CheckForDoubleing(i, dataBaseAccess, assignment));
                CheckForAllNewOnesIn(i, controller, dataBaseAccess);
            }
        }

        private bool CheckForDoubleing(int round, DataBaseAccess dataBaseAccess, Assignment assignment)
        {
            List<RaR2> rar2round = dataBaseAccess.readRaR2Round(round);
            if (rar2round.Count > 0)
            {
                return assignment.checkRoundsForDoubles(rar2round) != 0;
            }
            else
            {
                List<RaR3> raR3Round = dataBaseAccess.readRaR3Round(round);
                return assignment.checkRoundsForDoubles(raR3Round) != 0;
            }
        }

        private void CheckForAllNewOnesIn(int round, TheBigC controller, DataBaseAccess dataBaseAccess)
        {
            List<Participant> newParticipants = controller.GetParticipants().FindAll(p => !p.Old);
            var rar2s = dataBaseAccess.readRaR2Round(round);
            List<Participant> participants = new List<Participant>();
            if (rar2s.Count > 0)
            {
                foreach (var rar in rar2s)
                {
                    participants.Add(rar.OldParticipant);
                    participants.Add(rar.NewParticipant);
                }
            }
            else
            {
                var rar3s = dataBaseAccess.readRaR3Round(round);
                foreach (var rar in rar3s)
                {
                    participants.Add(rar.OldParticipant);
                    participants.Add(rar.NewParticipant);
                    if (rar.EitherParticipant != null)
                    {
                        participants.Add(rar.EitherParticipant);
                    }
                }
            }
            foreach (Participant p in newParticipants)
            {
                Assert.Contains(p, participants);
            }
        }

        [Fact]
        public void CreateRoundsWorksWith12OldAnd12NewAndRound1And2As2er()
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            Assignment assignment = new Assignment(dataBaseAccess);
            RaRModel raRModel = new RaRModel(assignment, dataBaseAccess);
            TheBigC controller = new TheBigC(raRModel);
            raRModel.controller = controller;

            controller.resetDatabase();

            var testParticipants = new List<Participant>
            {
                new Participant("Hannah", true, true, true, true, true, true),
                new Participant("Elli", true, true, true, true, true, true),
                new Participant("Paul", true, true, true, true, true, true),
                new Participant("Jake", true, true, true, true, true, true),
                new Participant("Petra", true, true, true, true, true, true),
                new Participant("Laurin", true, true, true, true, true, true),
                new Participant("Gregor", true, true, true, true, true, true),
                new Participant("Sina", true, true, true, true, true, true),
                new Participant("Johanna", true, true, true, true, true, true),
                new Participant("Gloria", true, true, true, true, true, true),
                new Participant("Fabienne", true, true, true, true, true, true),
                new Participant("Eline", true, true, true, true, true, true),

                new Participant("Konstantin", false, true, true, true, true, true),
                new Participant("Kirsten", false, true, true, true, true, true),
                new Participant("Janis", false, true, true, true, true, true),
                new Participant("Alex", false, true, true, true, true, true),
                new Participant("Pascal", false, true, true, true, true, true),
                new Participant("Phillipe", false, true, true, true, true, true),
                new Participant("Ari", false, true, true, true, true, true),
                new Participant("Paulina", false, true, true, true, true, true),
                new Participant("jana", false, true, true, true, true, true),
                new Participant("Anja", false, true, true, true, true, true),
                new Participant("Klaus", false, true, true, true, true, true),
                new Participant("Viktor", false, true, true, true, true, true)
            };

            foreach (Participant participant in testParticipants)
            {
                controller.AddParticipant(participant);
            }
            controller.setRoundPlan(new bool[] { true, false, false, true, false });
            controller.createRounds();
            Assert.True(controller.getRaR2Round(1).Count == 12);
            Assert.True(controller.getRaR2Round(4).Count == 12);
            Assert.True(controller.getRaR3Round(2).Count == 8);
            Assert.True(controller.getRaR3Round(3).Count == 8);
            Assert.True(controller.getRaR3Round(5).Count == 8);
            for (int i = 1; i <= 5; i++)
            {
                Assert.True(CheckForDoubleing(i, dataBaseAccess, assignment));
                CheckForAllNewOnesIn(i, controller, dataBaseAccess);
            }
        }

        [Fact]
        public void CreateRoundsWorksWith12OldAnd12NewAndAllRoundsAs3er()
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            Assignment assignment = new Assignment(dataBaseAccess);
            RaRModel raRModel = new RaRModel(assignment, dataBaseAccess);
            TheBigC controller = new TheBigC(raRModel);
            raRModel.controller = controller;

            controller.resetDatabase();

            var testParticipants = new List<Participant>
            {
                new Participant("Hannah", true, true, true, true, true, true),
                new Participant("Elli", true, true, true, true, true, true),
                new Participant("Paul", true, true, true, true, true, true),
                new Participant("Jake", true, true, true, true, true, true),
                new Participant("Petra", true, true, true, true, true, true),
                new Participant("Laurin", true, true, true, true, true, true),
                new Participant("Gregor", true, true, true, true, true, true),
                new Participant("Sina", true, true, true, true, true, true),
                new Participant("Johanna", true, true, true, true, true, true),
                new Participant("Gloria", true, true, true, true, true, true),
                new Participant("Fabienne", true, true, true, true, true, true),
                new Participant("Eline", true, true, true, true, true, true),

                new Participant("Konstantin", false, true, true, true, true, true),
                new Participant("Kirsten", false, true, true, true, true, true),
                new Participant("Janis", false, true, true, true, true, true),
                new Participant("Alex", false, true, true, true, true, true),
                new Participant("Pascal", false, true, true, true, true, true),
                new Participant("Phillipe", false, true, true, true, true, true),
                new Participant("Ari", false, true, true, true, true, true),
                new Participant("Paulina", false, true, true, true, true, true),
                new Participant("jana", false, true, true, true, true, true),
                new Participant("Anja", false, true, true, true, true, true),
                new Participant("Klaus", false, true, true, true, true, true),
                new Participant("Viktor", false, true, true, true, true, true)
            };

            foreach (Participant participant in testParticipants)
            {
                controller.AddParticipant(participant);
            }
            controller.setRoundPlan(new bool[] { false, false, false, false, false });
            controller.createRounds();
            Assert.True(controller.getRaR3Round(1).Count == 8);
            Assert.True(controller.getRaR3Round(4).Count == 8);
            Assert.True(controller.getRaR3Round(2).Count == 8);
            Assert.True(controller.getRaR3Round(3).Count == 8);
            Assert.True(controller.getRaR3Round(5).Count == 8);
            for (int i = 1; i <= 5; i++)
            {
                Assert.True(CheckForDoubleing(i, dataBaseAccess, assignment));
                CheckForAllNewOnesIn(i, controller, dataBaseAccess);
            }
        }

        [Fact]
        public void CreateRoundsWorksWith12OldAnd12NewAndAllRoundsAs2er()
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            Assignment assignment = new Assignment(dataBaseAccess);
            RaRModel raRModel = new RaRModel(assignment, dataBaseAccess);
            TheBigC controller = new TheBigC(raRModel);
            raRModel.controller = controller;

            controller.resetDatabase();

            var testParticipants = new List<Participant>
            {
                new Participant("Hannah", true, true, true, true, true, true),
                new Participant("Elli", true, true, true, true, true, true),
                new Participant("Paul", true, true, true, true, true, true),
                new Participant("Jake", true, true, true, true, true, true),
                new Participant("Petra", true, true, true, true, true, true),
                new Participant("Laurin", true, true, true, true, true, true),
                new Participant("Gregor", true, true, true, true, true, true),
                new Participant("Sina", true, true, true, true, true, true),
                new Participant("Johanna", true, true, true, true, true, true),
                new Participant("Gloria", true, true, true, true, true, true),
                new Participant("Fabienne", true, true, true, true, true, true),
                new Participant("Eline", true, true, true, true, true, true),

                new Participant("Konstantin", false, true, true, true, true, true),
                new Participant("Kirsten", false, true, true, true, true, true),
                new Participant("Janis", false, true, true, true, true, true),
                new Participant("Alex", false, true, true, true, true, true),
                new Participant("Pascal", false, true, true, true, true, true),
                new Participant("Phillipe", false, true, true, true, true, true),
                new Participant("Ari", false, true, true, true, true, true),
                new Participant("Paulina", false, true, true, true, true, true),
                new Participant("jana", false, true, true, true, true, true),
                new Participant("Anja", false, true, true, true, true, true),
                new Participant("Klaus", false, true, true, true, true, true),
                new Participant("Viktor", false, true, true, true, true, true)
            };

            foreach (Participant participant in testParticipants)
            {
                controller.AddParticipant(participant);
            }
            controller.setRoundPlan(new bool[] { true, true, true, true, true });
            controller.createRounds();
            Assert.True(controller.getRaR2Round(1).Count == 12);
            Assert.True(controller.getRaR2Round(4).Count == 12);
            Assert.True(controller.getRaR2Round(2).Count == 12);
            Assert.True(controller.getRaR2Round(3).Count == 12);
            Assert.True(controller.getRaR2Round(5).Count == 12);
            for (int i = 1; i <= 5; i++)
            {
                Assert.True(CheckForDoubleing(i, dataBaseAccess, assignment));
                CheckForAllNewOnesIn(i, controller, dataBaseAccess);
            }
        }

        [Fact]
        public void CreateRoundsWorksWith8OldAnd12NewAndAllRoundsAs3er()
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            Assignment assignment = new Assignment(dataBaseAccess);
            RaRModel raRModel = new RaRModel(assignment, dataBaseAccess);
            TheBigC controller = new TheBigC(raRModel);
            raRModel.controller = controller;

            controller.resetDatabase();

            var testParticipants = new List<Participant>
            {
                new Participant("Hannah", true, true, true, true, true, true),
                new Participant("Elli", true, true, true, true, true, true),
                new Participant("Paul", true, true, true, true, true, true),
                new Participant("Jake", true, true, true, true, true, true),
                new Participant("Petra", true, true, true, true, true, true),
                new Participant("Laurin", true, true, true, true, true, true),
                new Participant("Gregor", true, true, true, true, true, true),
                new Participant("Sina", true, true, true, true, true, true),

                new Participant("Konstantin", false, true, true, true, true, true),
                new Participant("Kirsten", false, true, true, true, true, true),
                new Participant("Janis", false, true, true, true, true, true),
                new Participant("Alex", false, true, true, true, true, true),
                new Participant("Pascal", false, true, true, true, true, true),
                new Participant("Phillipe", false, true, true, true, true, true),
                new Participant("Ari", false, true, true, true, true, true),
                new Participant("Paulina", false, true, true, true, true, true),
                new Participant("jana", false, true, true, true, true, true),
                new Participant("Anja", false, true, true, true, true, true),
                new Participant("Klaus", false, true, true, true, true, true),
                new Participant("Viktor", false, true, true, true, true, true)
            };

            foreach (Participant participant in testParticipants)
            {
                controller.AddParticipant(participant);
            }
            controller.setRoundPlan(new bool[] { true, false, false, true, false });
            controller.createRounds();
            Assert.True(controller.getRaR3Round(1).Count == 6);
            Assert.True(controller.getRaR3Round(4).Count == 6);
            Assert.True(controller.getRaR3Round(2).Count == 6);
            Assert.True(controller.getRaR3Round(3).Count == 6);
            Assert.True(controller.getRaR3Round(5).Count == 6);
            for (int i = 1; i <= 5; i++)
            {
                Assert.True(CheckForDoubleing(i, dataBaseAccess, assignment));
                CheckForAllNewOnesIn(i, controller, dataBaseAccess);
            }
        }
    }
}