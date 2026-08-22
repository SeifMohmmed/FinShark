import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import type { CompanyProfile } from "../../company";
import { getCompanyProfile } from "../../api";

interface Props {}

const CompanyPage = (props: Props) => {
  //useParams retrieves the parameter that was passed through the URL.
  // route: /company/:ticker
  let { ticker } = useParams();

  const [company, setCompay] = useState<CompanyProfile>();

  useEffect(() => {
    const getProfileInit = async () => {
      const result = await getCompanyProfile(ticker!);
      setCompay(result?.data[0]);
    };
    getProfileInit();
  }, []);
  return (
    <>
      {company ? (
        <div className="company-profile-container">{company.companyName}</div>
      ) : (
        <div>Company Not Found!</div>
      )}
    </>
  );
};

export default CompanyPage;
