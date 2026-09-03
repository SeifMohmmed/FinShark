import React, { useEffect, useState } from "react";
import type { CompanyKeyMetrics } from "../../company";
import { useOutletContext } from "react-router-dom";
import { getKeyMetrics } from "../../api";
import RatioList from "../RatioList/RatioList";
import Spinner from "../Spinner/Spinner";
import {
  formatLargeMonetaryNumber,
  formatRatio,
} from "../../Helpers/NumberFormatting";
import StockComment from "../StockComment/StockComment";

const tableConfig = [
  {
    label: "Market Cap",
    subTitle: "Total market value of the company",
    render: (company: CompanyKeyMetrics) =>
      formatLargeMonetaryNumber(company.marketCapTTM),
  },
  {
    label: "Current Ratio",
    subTitle: "Ability to cover short-term liabilities",
    render: (company: CompanyKeyMetrics) =>
      formatRatio(company.currentRatioTTM),
  },
  {
    label: "Return On Equity",
    subTitle: "Return generated on shareholders' equity",
    render: (company: CompanyKeyMetrics) => formatRatio(company.roeTTM),
  },
  {
    label: "Cash Per Share",
    subTitle: "Cash available for each share",
    render: (company: CompanyKeyMetrics) =>
      formatLargeMonetaryNumber(company.cashPerShareTTM),
  },
];

type Props = {};

const CompanyProfile = (props: Props) => {
  const ticker = useOutletContext<string>();
  const [companyData, setCompanyData] = useState<CompanyKeyMetrics>();

  useEffect(() => {
    const getCompanyKeyRatios = async () => {
      const value = await getKeyMetrics(ticker);
      setCompanyData(value?.data[0]);
    };
    getCompanyKeyRatios();
  }, []);

  return (
    <>
      {companyData ? (
        <>
          <RatioList config={tableConfig} data={companyData} />
          <StockComment stockSymbol={ticker} />
        </>
      ) : (
        <Spinner />
      )}
    </>
  );
};

export default CompanyProfile;
