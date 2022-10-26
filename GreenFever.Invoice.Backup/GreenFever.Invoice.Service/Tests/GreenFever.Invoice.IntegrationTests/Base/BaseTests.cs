using System;
using GreenFever.Invoice.BusinessLogic.Interfaces;
using GreenFever.Invoice.BusinessLogic.Services;
using GreenFever.Invoice.Dal.Interfaces;
using GreenFever.Invoice.Dto.Enums;
using GreenFever.Invoice.Dto.Objects;
using GreenFever.Invoice.Utils.Configuration;
using Microsoft.Extensions.Options;
using Moq;

namespace GreenFever.Invoice.IntegrationTests.Base
{
    public class BaseTests
    {
        public BaseTests()
        {
            this.InitializeMocks();
            this.InitializeObjects();
        }

        protected IInvoicesProcessingService ProcessingService { get; private set; }

        protected Mock<IInvoicesProcessingRepository> ProcessingRepositoryMock { get; private set; }

        protected InvoiceProcessingSetting ProcessingSetting { get; private set; }

        protected int PageSize { get; } = 1000;

        protected int InvoiceCount { get; } = 51423;

        protected int SettlementCount { get; } = 63524;

        protected int CountForManualGrouping { get; } = 324;

        protected int RunId { get; } = 134;

        protected DateTime TestRunDate { get; } = DateTime.Now;

        protected ProcessingPercentageInfo TestPercentageInfo { get; } = new ProcessingPercentageInfo
        {
            Percent = 50,
            ProcessedItemsCount = 100,
            TotalItemsCount = 200
        };

        private void InitializeMocks()
        {
            this.ProcessingRepositoryMock = new Mock<IInvoicesProcessingRepository>();
            this.ProcessingRepositoryMock.Setup(x => x.GetTotalInvoicesCountForSettlementType()).Returns(this.SettlementCount);
            this.ProcessingRepositoryMock.Setup(x => x.GetTotalInvoicesCountForInvoiceType()).Returns(this.InvoiceCount);
            this.ProcessingRepositoryMock.Setup(x => x.IsBatchInvoicesProcessingRunAvailable()).Returns(true);
            this.ProcessingRepositoryMock.Setup(x => x.StartBatchInvoiceProcessingRun(
                It.IsAny<InvoiceType>(),
                It.IsAny<DateTime>(),
                It.IsAny<int>())).Returns(this.RunId);
            this.ProcessingRepositoryMock.Setup(x => x.FinishBatchInvoiceProcessingRun(It.Is<int>(y => y == this.RunId)));
            this.ProcessingRepositoryMock.Setup(x => x.ExecuteBatchInvoicesProcessingRun(
                It.IsAny<InvoiceType>(),
                It.IsAny<DateTime>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>())).Returns(new InvoicesProcessingResult());
            this.ProcessingRepositoryMock.Setup(x => x.GetBatchProcessingPercentageInfo(It.Is<int>(y => y == this.RunId)))
                .Returns(this.TestPercentageInfo);

            this.ProcessingRepositoryMock.Setup(x => x.IsSingleInvoiceProcessingRunAvailable(It.IsAny<int>())).Returns(true);
            this.ProcessingRepositoryMock.Setup(x => x.ExecuteSingleInvoiceProcessingRunForSettlementType(
                It.IsAny<int>(),
                It.IsAny<DateTime>())).Returns(new InvoicesProcessingResult());
            this.ProcessingRepositoryMock.Setup(x => x.ExecuteSingleInvoiceProcessingRunForInvoiceType(
                It.IsAny<int>(),
                It.IsAny<DateTime>())).Returns(new InvoicesProcessingResult());

            this.ProcessingRepositoryMock.Setup(x => x.ExecuteManualInvoiceGrouping(It.IsAny<DateTime>()));
            this.ProcessingRepositoryMock.Setup(x => x.GetCountOfInvoicesForManualGrouping()).Returns(this.CountForManualGrouping);
        }

        private void InitializeObjects()
        {
            this.ProcessingSetting = new InvoiceProcessingSetting { BatchSize = 1000 };

            var optionsMock = new Mock<IOptions<InvoiceProcessingSetting>>();
            optionsMock.Setup(x => x.Value).Returns(this.ProcessingSetting);

            this.ProcessingService = new InvoicesProcessingService(this.ProcessingRepositoryMock.Object, optionsMock.Object);
        }        
    }
}
