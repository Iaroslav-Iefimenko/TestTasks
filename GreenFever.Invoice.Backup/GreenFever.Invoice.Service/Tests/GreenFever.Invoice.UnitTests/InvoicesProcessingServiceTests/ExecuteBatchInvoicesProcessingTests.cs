using System;
using GreenFever.Invoice.BusinessLogic.Services;
using GreenFever.Invoice.Dto.Enums;
using GreenFever.Invoice.Dto.Objects;
using GreenFever.Invoice.UnitTests.Base;
using Moq;
using NUnit.Framework;

namespace GreenFever.Invoice.UnitTests.InvoicesProcessingServiceTests
{
    [TestFixture]
    public class ExecuteBatchInvoicesProcessingTests : BaseTests
    {
        private readonly InvoicesProcessingService processingService;

        private InvoiceType testInvoiceType = InvoiceType.Settlement;
        private int pagesCount;

        public ExecuteBatchInvoicesProcessingTests()
        {
            this.pagesCount = this.PageSize == 0 ? 0 : (this.SettlementCount / this.PageSize) + 1;

            this.processingService = new InvoicesProcessingService(
                this.ProcessingRepositoryMock.Object,
                this.OptionsMock.Object);            
        }

        [Test]
        public void Positive()
        {
            // arrange
            this.SetupMockMethods(-1, true, false);

            // act
            var result = this.processingService.ExecuteBatchInvoicesProcessingRun(this.testInvoiceType, this.TestRunDate);

            // assert
            this.ProcessingRepositoryMock.Verify(
                x => x.IsBatchInvoicesProcessingRunAvailable(),
                Times.Once,
                "IsBatchInvoicesProcessingRunAvailable function is not called");
            this.ProcessingRepositoryMock.Verify(
                x => x.GetTotalInvoicesCountForSettlementType(),
                Times.Once,
                "GetTotalInvoicesCountForSettlementType function is not called");
            this.ProcessingRepositoryMock.Verify(
                x => x.StartBatchInvoiceProcessingRun(
                    It.Is<InvoiceType>(p => p == this.testInvoiceType),
                    It.Is<DateTime>(p => p == this.TestRunDate),
                    It.Is<int>(p => p == this.SettlementCount)),
                Times.Once,
                "StartBatchInvoiceProcessingRun function is not called");
            this.ProcessingRepositoryMock.Verify(
                x => x.ExecuteBatchInvoicesProcessingRun(
                    It.Is<InvoiceType>(p => p == this.testInvoiceType),
                    It.Is<DateTime>(p => p == this.TestRunDate),
                    It.Is<int>(p => p == this.RunId),
                    It.Is<int>(p => p >= 0 && p < this.pagesCount),
                    It.Is<int>(p => p == this.PageSize)),
                Times.Exactly(this.pagesCount),
                "ExecuteBatchInvoicesProcessingRun function is not called or called incorrect times");
            this.ProcessingRepositoryMock.Verify(
                x => x.FinishBatchInvoiceProcessingRun(It.Is<int>(p => p == this.RunId)),
                Times.Once,
                "FinishBatchInvoiceProcessingRun function is not called");

            Assert.False(result.ContainsErrors, "Result of ExecuteBatchInvoicesProcessingRun call contains incorrect errors flag");
            Assert.AreEqual(0, result.ClientId, "Result of ExecuteBatchInvoicesProcessingRun call contains incorrect client Id");
        }

        [Test]
        public void PositiveWithOracleErrorMessages()
        {
            // arrange
            this.SetupMockMethods(-1, true, true);            

            // act
            var result = this.processingService.ExecuteBatchInvoicesProcessingRun(this.testInvoiceType, this.TestRunDate);

            // assert
            this.ProcessingRepositoryMock.Verify(
                x => x.IsBatchInvoicesProcessingRunAvailable(),
                Times.Once,
                "IsBatchInvoicesProcessingRunAvailable function is not called");
            this.ProcessingRepositoryMock.Verify(
                x => x.GetTotalInvoicesCountForSettlementType(),
                Times.Once,
                "GetTotalInvoicesCountForSettlementType function is not called");
            this.ProcessingRepositoryMock.Verify(
                x => x.StartBatchInvoiceProcessingRun(
                    It.Is<InvoiceType>(p => p == this.testInvoiceType),
                    It.Is<DateTime>(p => p == this.TestRunDate),
                    It.Is<int>(p => p == this.SettlementCount)),
                Times.Once,
                "StartBatchInvoiceProcessingRun function is not called");
            this.ProcessingRepositoryMock.Verify(
                x => x.ExecuteBatchInvoicesProcessingRun(
                    It.Is<InvoiceType>(p => p == this.testInvoiceType),
                    It.Is<DateTime>(p => p == this.TestRunDate),
                    It.Is<int>(p => p == this.RunId),
                    It.Is<int>(p => p >= 0 && p < this.pagesCount),
                    It.Is<int>(p => p == this.PageSize)),
                Times.Exactly(this.pagesCount),
                "ExecuteBatchInvoicesProcessingRun function is not called or called incorrect times");
            this.ProcessingRepositoryMock.Verify(
                x => x.FinishBatchInvoiceProcessingRun(It.Is<int>(p => p == this.RunId)),
                Times.Once,
                "FinishBatchInvoiceProcessingRun function is not called");

            Assert.AreEqual(0, result.ClientId, "Result of ExecuteBatchInvoicesProcessingRun call contains incorrect client Id");
            Assert.True(result.ContainsErrors, "Result of ExecuteBatchInvoicesProcessingRun call contains incorrect errors flag");
            Assert.AreEqual(this.pagesCount, result.EANsWithoutSLPcode.Count);
            Assert.AreEqual("test " + nameof(result.EANsWithoutSLPcode), result.EANsWithoutSLPcode[0]);
            Assert.AreEqual(this.pagesCount, result.EANsDouble.Count);
            Assert.AreEqual("test " + nameof(result.EANsDouble), result.EANsDouble[0]);
            Assert.AreEqual(this.pagesCount, result.SLPcodesMissing.Count);
            Assert.AreEqual("test " + nameof(result.SLPcodesMissing), result.SLPcodesMissing[0]);
            Assert.AreEqual(this.pagesCount, result.Invoices.Count);
            Assert.AreEqual("test " + nameof(result.Invoices), result.Invoices[0]);
            Assert.AreEqual(this.pagesCount, result.PdfErrors.Count);
            Assert.AreEqual("test " + nameof(result.PdfErrors), result.PdfErrors[0]);
        }

        [Test]
        public void NegativeNotProcessCommissie()
        {
            // arrange
            this.SetupMockMethods(-1, true, false);
            this.testInvoiceType = InvoiceType.Commission;

            // act
            var result = this.processingService.ExecuteBatchInvoicesProcessingRun(this.testInvoiceType, this.TestRunDate);

            // assert
            this.ProcessingRepositoryMock.Verify(
                x => x.IsBatchInvoicesProcessingRunAvailable(),
                Times.Never,
                "IsBatchInvoicesProcessingRunAvailable function is called");
            this.ProcessingRepositoryMock.Verify(
                x => x.GetTotalInvoicesCountForSettlementType(),
                Times.Never,
                "GetTotalInvoicesCountForSettlementType function is called");
            this.ProcessingRepositoryMock.Verify(
                x => x.GetTotalInvoicesCountForInvoiceType(),
                Times.Never,
                "GetTotalInvoicesCountForInvoiceType function is called");
            this.ProcessingRepositoryMock.Verify(
                x => x.StartBatchInvoiceProcessingRun(It.IsAny<InvoiceType>(), It.IsAny<DateTime>(), It.IsAny<int>()),
                Times.Never,
                "StartBatchInvoiceProcessingRun function is called");
            this.ProcessingRepositoryMock.Verify(
                x => x.ExecuteBatchInvoicesProcessingRun(
                    It.IsAny<InvoiceType>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()),
                Times.Never,
                "ExecuteBatchInvoicesProcessingRun function is called");
            this.ProcessingRepositoryMock.Verify(
                x => x.FinishBatchInvoiceProcessingRun(It.IsAny<int>()),
                Times.Never,
                "FinishBatchInvoiceProcessingRun function is called");

            Assert.True(result.ContainsErrors, "Result of ExecuteBatchInvoicesProcessingRun call contains incorrect errors flag");
            Assert.AreEqual(0, result.ClientId, "Result of ExecuteBatchInvoicesProcessingRun call contains incorrect client Id");
        }

        [Test]
        public void NegativeNotStartTwoRunsSimultaneously()
        {
            // arrange
            this.SetupMockMethods(-1, false, false);

            // act
            var result = this.processingService.ExecuteBatchInvoicesProcessingRun(this.testInvoiceType, this.TestRunDate);

            // assert
            this.ProcessingRepositoryMock.Verify(
                x => x.IsBatchInvoicesProcessingRunAvailable(),
                Times.Once,
                "IsBatchInvoicesProcessingRunAvailable function is not called");
            this.ProcessingRepositoryMock.Verify(
                x => x.GetTotalInvoicesCountForSettlementType(),
                Times.Never,
                "GetTotalInvoicesCountForSettlementType function is called");
            this.ProcessingRepositoryMock.Verify(
                x => x.GetTotalInvoicesCountForInvoiceType(),
                Times.Never,
                "GetTotalInvoicesCountForInvoiceType function is called");
            this.ProcessingRepositoryMock.Verify(
                x => x.StartBatchInvoiceProcessingRun(It.IsAny<InvoiceType>(), It.IsAny<DateTime>(), It.IsAny<int>()),
                Times.Never,
                "StartBatchInvoiceProcessingRun function is called");
            this.ProcessingRepositoryMock.Verify(
                x => x.ExecuteBatchInvoicesProcessingRun(
                    It.IsAny<InvoiceType>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()),
                Times.Never,
                "ExecuteBatchInvoicesProcessingRun function is called");
            this.ProcessingRepositoryMock.Verify(
                x => x.FinishBatchInvoiceProcessingRun(It.IsAny<int>()),
                Times.Never,
                "FinishBatchInvoiceProcessingRun function is called");

            Assert.True(result.ContainsErrors, "Result of ExecuteBatchInvoicesProcessingRun call contains incorrect errors flag");
            Assert.AreEqual(0, result.ClientId, "Result of ExecuteBatchInvoicesProcessingRun call contains incorrect client Id");
        }

        [Test]
        public void NegativeNotExecuteProcessingForZeroRecords()
        {
            // arrange
            this.SetupMockMethods(0, true, false);

            // act
            var result = this.processingService.ExecuteBatchInvoicesProcessingRun(this.testInvoiceType, this.TestRunDate);

            // assert
            this.ProcessingRepositoryMock.Verify(
                x => x.IsBatchInvoicesProcessingRunAvailable(),
                Times.Once,
                "IsBatchInvoicesProcessingRunAvailable function is not called");
            this.ProcessingRepositoryMock.Verify(
                x => x.GetTotalInvoicesCountForSettlementType(),
                Times.Once,
                "GetTotalInvoicesCountForSettlementType function is not called");
            this.ProcessingRepositoryMock.Verify(
                x => x.GetTotalInvoicesCountForInvoiceType(),
                Times.Never,
                "GetTotalInvoicesCountForInvoiceType function is called");
            this.ProcessingRepositoryMock.Verify(
                x => x.StartBatchInvoiceProcessingRun(It.IsAny<InvoiceType>(), It.IsAny<DateTime>(), It.IsAny<int>()),
                Times.Never,
                "StartBatchInvoiceProcessingRun function is called");
            this.ProcessingRepositoryMock.Verify(
                x => x.ExecuteBatchInvoicesProcessingRun(
                    It.IsAny<InvoiceType>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()),
                Times.Never,
                "ExecuteBatchInvoicesProcessingRun function is called");
            this.ProcessingRepositoryMock.Verify(
                x => x.FinishBatchInvoiceProcessingRun(It.IsAny<int>()),
                Times.Never,
                "FinishBatchInvoiceProcessingRun function is called");

            Assert.True(result.ContainsErrors, "Result of ExecuteBatchInvoicesProcessingRun call contains incorrect errors flag");
            Assert.AreEqual(0, result.ClientId, "Result of ExecuteBatchInvoicesProcessingRun call contains incorrect client Id");
        }

        private void SetupMockMethods(int settlementCount, bool isBatchAvailable, bool addErrors)
        {
            this.ProcessingRepositoryMock.Invocations.Clear();
            testInvoiceType = InvoiceType.Settlement;

            this.ProcessingRepositoryMock.Setup(x => x.GetTotalInvoicesCountForSettlementType())
                .Returns(settlementCount < 0 ? this.SettlementCount : settlementCount);
            this.ProcessingRepositoryMock.Setup(x => x.GetTotalInvoicesCountForInvoiceType()).Returns(this.InvoiceCount);
            this.ProcessingRepositoryMock.Setup(x => x.IsBatchInvoicesProcessingRunAvailable()).Returns(isBatchAvailable);

            if (addErrors)
            {
                this.ProcessingRepositoryMock.Setup(x => x.ExecuteBatchInvoicesProcessingRun(
                It.IsAny<InvoiceType>(),
                It.IsAny<DateTime>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>())).Returns(() =>
                {
                    var res = new InvoicesProcessingResult();
                    res.ContainsErrors = true;
                    res.EANsWithoutSLPcode.Add("test " + nameof(res.EANsWithoutSLPcode));
                    res.EANsDouble.Add("test " + nameof(res.EANsDouble));
                    res.SLPcodesMissing.Add("test " + nameof(res.SLPcodesMissing));
                    res.Invoices.Add("test " + nameof(res.Invoices));
                    res.PdfErrors.Add("test " + nameof(res.PdfErrors));
                    return res;
                });
            }
            else
            {
                this.ProcessingRepositoryMock.Setup(x => x.ExecuteBatchInvoicesProcessingRun(
                    It.IsAny<InvoiceType>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>())).Returns(new InvoicesProcessingResult());
            }
        }
    }
}
