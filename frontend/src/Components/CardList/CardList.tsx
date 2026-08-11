import Card from "../Card/Card";

interface Props {}

export const CardList: React.FC<Props> = (props: Props) => {
  return (
    <div>
      <Card companyName="Apple Inc." ticker="AAPL" price={110} />
      <Card companyName="Microsoft Corporation" ticker="MSFT" price={200} />
      <Card companyName="Amazon.com Inc." ticker="AMZN" price={100} />
    </div>
  );
};
