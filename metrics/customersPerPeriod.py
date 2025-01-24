from model import Employee
from model.customer import Customer

import matplotlib
matplotlib.use('Agg')

import pandas as pd
import matplotlib.pyplot as plt
import matplotlib.dates as mdates
from datetime import datetime, timedelta
import io

from metrics.graph_semaphore import semaphore

def generate_graph1(period, session):
    semaphore.acquire()
    query = session.query(Customer.joined_on).all()

    df = pd.DataFrame(query, columns=['joined_on'])
    df['joined_on'] = pd.to_datetime(df['joined_on'])

    now = datetime.now()

    if period == '7D':
        start_date = now - timedelta(weeks=1)
    elif period == '1M':
        start_date = now - timedelta(days=30)
    elif period == '3M':
        start_date = now - timedelta(days=90)
    elif period == 'All':
        start_date = datetime(1970, 1, 1)

    df_period = df[df['joined_on'] >= start_date]
    df_period.set_index('joined_on', inplace=True)

    if len(df_period) <= 0:
        img = io.BytesIO()
        plt.figure(figsize=(10, 6))
        plt.text(0.5, 0.5, 'No data available for this period', fontsize=18, ha='center')
        plt.axis('off')
        plt.savefig(img, format='png')
        img.seek(0)
        plt.close()
        semaphore.release()
        return img

    if period == '7D':
        start_date = now - timedelta(weeks=1)
        df_counts = df_period.resample('D').size()
    elif period == '1M':
        start_date = now - timedelta(days=30)
        df_counts = df_period.resample('2D').size()
    elif period == '3M':
        start_date = now - timedelta(days=90)
        df_counts = df_period.resample('5D').size()
    elif period == 'All':
        start_date = datetime(1970, 1, 1)
        df_counts = df_period.resample('W').size()

    plt.figure(figsize=(10, 6))

    plt.plot(df_counts.index, df_counts.values, color='#007bff', linewidth=2)
    plt.plot(df_counts.index, df_counts.rolling(window=3).mean(), color='#007bff', linestyle='--', linewidth=2)
    plt.fill_between(df_counts.index, df_counts.values, color='#007bff', alpha=0.1)
    plt.gca().xaxis.set_major_formatter(mdates.DateFormatter('%d %b %Y'))
    plt.gcf().autofmt_xdate()
    plt.ylim(0)
    plt.grid(True, which='major', axis='y', linestyle='--', linewidth=0.5)
    plt.legend()
    plt.tight_layout()
    img = io.BytesIO()
    plt.savefig(img, format='png')
    img.seek(0)
    plt.close()
    semaphore.release()
    return img


def get_customer_stats(session):
    total_customers = session.query(Customer).count()
    total_coaches = session.query(Employee).count()

    avg_customers_per_coach = total_customers / total_coaches

    return {
        "total_customers": total_customers,
        "avg_customers_per_coach": avg_customers_per_coach
    }