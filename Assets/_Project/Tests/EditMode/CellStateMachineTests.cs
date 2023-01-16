using System.Threading.Tasks;
using NUnit.Framework;
using SnekTech.GridCell.FSM;
using Tests.EditMode.Builder;

namespace Tests.EditMode
{
    public class CellStateMachineTests
    {
        [Test]
        public void cell_should_be_covered_by_default()
        {
            var fsm = new CellFSM(A.Cell);

            Assert.AreSame(fsm.Current, fsm.CoveredState);
        }

        [Test]
        public async Task covered_cell_should_be_revealed_on_primary()
        {
            var fsm = new CellFSM(A.Cell);
            fsm.ChangeState(fsm.RevealedState);

            await fsm.OnPrimary();
            Assert.AreSame(fsm.Current, fsm.RevealedState);
        }

        [Test]
        public async Task covered_cell_should_be_flagged_on_secondary()
        {
            var fsm = new CellFSM(A.Cell);

            await fsm.OnSecondary();

            Assert.AreSame(fsm.Current, fsm.FlaggedState);
        }

        [Test]
        public async Task flagged_cell_should_be_covered_on_secondary()
        {
            var fsm = new CellFSM(A.Cell);
            fsm.ChangeState(fsm.FlaggedState);

            await fsm.OnSecondary();
            Assert.AreSame(fsm.Current, fsm.CoveredState);
        }
    }
}