import React, { useEffect, useState } from "react";
import type { CompanyTenK } from "../../company";
import { getTenKFilings } from "../../api";
import Spinner from "../Spinner/Spinner";
import TenKFinderItem from "../TenKFinderItem/TenKFinderItem";

type Props = {
  ticker: string;
};

const TenKFinder = ({ ticker }: Props) => {
  const [companyData, setCompanyData] = useState<CompanyTenK[]>([]);
  useEffect(() => {
    const getTenKData = async () => {
      const value = await getTenKFilings(ticker);
      setCompanyData(value);
    };
    getTenKData();
  }, [ticker]);
  return (
    <div className="inline-flex rounded-md shadow-sm m-4">
      {companyData ? (
        companyData?.slice(0, 5).map((tenK) => {
          return <TenKFinderItem tenK={tenK} />;
        })
      ) : (
        <Spinner />
      )}
    </div>
  );
};

export default TenKFinder;
