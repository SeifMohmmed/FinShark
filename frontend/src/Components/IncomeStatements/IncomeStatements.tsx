import { useOutletContext } from "react-router-dom";
import type { CompanyIncomeStatement } from "../../company";
import { useEffect, useState } from "react";
import { getIncomeStatement } from "../../api";
import Table from "../Table/Table";

type Props = {};

const configs = [
  {
    label: "Date",
    render: (company: CompanyIncomeStatement) => company.date,
  },
  {
    label: "Revenue",
    render: (company: CompanyIncomeStatement) => company.revenue,
  },
  {
    label: "Cost Of Revenue",
    render: (company: CompanyIncomeStatement) => company.costOfRevenue,
  },
  {
    label: "Depreciation",
    render: (company: CompanyIncomeStatement) =>
      company.depreciationAndAmortization,
  },
  {
    label: "Operating Income",
    render: (company: CompanyIncomeStatement) => company.operatingIncome,
  },
  {
    label: "Income Before Taxes",
    render: (company: CompanyIncomeStatement) => company.incomeBeforeTax,
  },
  {
    label: "Net Income",
    render: (company: CompanyIncomeStatement) => company.netIncome,
  },
  {
    label: "Earnings Per Share",
    render: (company: CompanyIncomeStatement) => company.eps,
  },
  {
    label: "Earnings Per Diluted",
    render: (company: CompanyIncomeStatement) => company.epsDiluted,
  },
];

const IncomeStatements = () => {
  const ticker = useOutletContext<string>();

  const [incomeStatement, setIncomeStatement] = useState<
    CompanyIncomeStatement[]
  >([]);

  useEffect(() => {
    const incomeStatementFetch = async () => {
      try {
        const result = await getIncomeStatement(ticker);

        setIncomeStatement(result);
      } catch (error) {
        console.error("Failed to fetch income statement:", error);
      }
    };

    incomeStatementFetch();
  }, [ticker]);

  return (
    <>
      {incomeStatement.length > 0 ? (
        <Table config={configs} data={incomeStatement} />
      ) : (
        <h1>Could not find income statement.</h1>
      )}
    </>
  );
};
export default IncomeStatements;
