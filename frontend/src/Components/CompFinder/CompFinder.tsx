import React, { useEffect, useState } from "react";
import type { CompanyCompData } from "../../company";
import { getStockPeers } from "../../api";
import CompFinderItem from "../CompFinderItem/CompFinderItem";

type Props = {
  ticker: string;
};

const CompFinder = ({ ticker }: Props) => {
  const [companyData, setCompanyData] = useState<CompanyCompData[]>([]);

  useEffect(() => {
    const getCompData = async () => {
      const result = await getStockPeers(ticker);
      setCompanyData(result);
    };
    getCompData();
  }, [ticker]);

  return (
    <div className="inline-flex rounded-md shadow-sm m-4">
      {companyData.map((company) => {
        return <CompFinderItem key={company.symbol} ticker={company.symbol} />;
      })}
    </div>
  );
};

export default CompFinder;
