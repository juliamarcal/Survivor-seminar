from model import Event
from model.customer import Customer

import matplotlib
matplotlib.use('Agg')

import pandas as pd
import matplotlib.pyplot as plt
import matplotlib.dates as mdates
from datetime import datetime
import io

from metrics.graph_semaphore import semaphore

def generate_graph2(session):
    semaphore.acquire()
    query = session.query(Event.date).all()

    df = pd.DataFrame(query, columns=['date'])
    df['date'] = pd.to_datetime(df['date'])

    df.set_index('date', inplace=True)

    if len(df) <= 0:
        img = io.BytesIO()
        plt.figure(figsize=(10, 6))
        plt.text(0.5, 0.5, 'No data available for this period', fontsize=18, ha='center')
        plt.axis('off')
        plt.savefig(img, format='png')
        img.seek(0)
        plt.close()
        semaphore.release()
        return img


    df_counts = df.resample('W').size()

    plt.figure(figsize=(10, 6))
    plt.bar(df_counts.index, df_counts.values, color='#007bff', alpha=0.7)
    plt.gca().xaxis.set_major_formatter(mdates.DateFormatter('%d %b %Y'))
    plt.gcf().autofmt_xdate()
    plt.ylim(0)
    plt.grid(True, which='major', axis='y', linestyle='--', linewidth=0.5)
    plt.tight_layout()

    img = io.BytesIO()
    plt.savefig(img, format='png')
    img.seek(0)
    plt.close()
    semaphore.release()
    return img


def generate_metrics_for_events(session):
    query = session.query(Event.date).all()

    df = pd.DataFrame(query, columns=['date'])
    df['date'] = pd.to_datetime(df['date'])
    df.set_index('date', inplace=True)

    now = datetime.now()

    if len(df) > 0:
        start_date = df.index.min()
        end_date = df.index.max()
        total_days = (end_date - start_date).days + 1

        total_events = len(df)

        daily_average = total_events / total_days
        weekly_average = daily_average * 7
        monthly_average = daily_average * 30
    else:
        daily_average = 0
        weekly_average = 0
        monthly_average = 0

    return {
        'monthly_average': monthly_average,
        'weekly_average': weekly_average,
        'daily_average': daily_average
    }
