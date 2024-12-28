import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-privacypolicy',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './privacypolicy.component.html',
  styleUrl: './privacypolicy.component.css'
})
export class PrivacypolicyComponent {
  activeIndex: number | null = null;

  // Dados dinâmicos das abas
  policyItems = [
    {
      title: 'O que são dados pessoais e que categorias de dados tratamos?',
      contents: [
        {
          type: 'text',
          content: 'De acordo com a Lei n.º 67/98, de 26 de outubro:'
        },
        {
          type: 'text',
          content: '"Dados pessoais": qualquer informação, de qualquer natureza e independentemente do respectivo suporte, incluindo som e image, relativa a uma pessoa singular identificada ou identificável ("titular dos dados").'
        },
        {
          type: 'text',
          content: 'De seguida tem exemplos de categorias de dados pessoais tratados pela nossa empresa:'
        },
        {
          type: 'table',
          row1name: 'Categoria',
          row2name: 'Dados',
          rows: [
            {
              category: 'Dados de identificação',
              examples:
                'Nome, idade, data de nascimento, género'
            },
            {
              category: 'Dados de comunicação',
              examples:
                'Email, número de telemóvel, contactos de emergência*'
            },
            {
              category: 'Perfil médico',
              examples:
                'Histórico médico e histórico de serviços que recebeu'
            }
          ]
        }
      ]
    },
    {
      title: 'Quem é o responsável pelo tratamento dos seus dados pessoais?',
      contents: [
        {
          type: 'text',
          content:
            'As seguintes entidades são as responsáveis pelo tratamento dos seus dados pessoais:'
        },
        {
          type: 'text',
          content:
            '- André Ferreira - 1190378@isep.ipp.pt'
        },
        {
          type: 'text',
          content:
            '- David Marques - 1221276@isep.ipp.pt'
        },
        {
          type: 'text',
          content:
            '- Diogo Cunha - 1221071@isep.ipp.pt'
        },
        {
          type: 'text',
          content:
            '- João Monteiro - 1221023@isep.ipp.pt'
        },
        {
          type: 'text',
          content:
            'Para cumprir com o artigo nº37 da RGPD, devido a sermos um organismo público, também temos um Encarregado de Proteção de Dados (DPO):'
        },
        {
          type: 'text',
          content:
            '- Diogo Cunha - 1221071@isep.ipp.pt'
        },
      ]
    },
    {
      title: 'Que tratamento é que os seus dados irão receber?',
      contents: [
        {
          type: 'text',
          content:
            'Os seus dados pessoais serão recolhidos, armazenados no sistema, destruidos (quando são removidos do sistema por seu pedido* ou por já não serem úteis) e utilização'
        }
      ]
    },
    {
      title: 'Para que finalidades é que tratamos os seus dados pessoais?',
      contents: [
        {
          type: 'text',
          content:
            'Os seus dados serão utilizados para as seguintes finalidades:'
        },
        {
          type: 'table',
          row1name: 'Finalidade',
          row2name: 'Explicação',
          rows: [
            {
              category: 'Comunicação',
              examples: 'O seu Email e Número de Telemóvel serão usados para comunicar consigo sobre a marcação de cirurgias, a alteração de dados sensíveis ou para obter a sua confirmação. Os Contactos de Emergência fornecidos por si também serão usados para isto, contudo apenas em situações de extrema necessidade'
            },
            {
              category: 'Personalização',
              examples: 'O seu Nome será usado para customizar a comunicação consigo no website ou em mensagens enviadas. A sua Data de Nascimento será usada para lhe aparecer informação mais relevante. O seu Género será usado nos dois aspetos anteriormente mencionados'
            },
            {
              category: 'Fins Estatísticos',
              examples: 'Estes dados, nomeadamente a Data de Nascimento, Género e Histórico Médico) serão usados para, por exemplo, termos uma maior ideia de qual gama de idades tendem a fazer um dado tipo de cirurgia'
            },
            {
              category: 'Monitorização do Sistema',
              examples: 'O seu Histórico Médico será usado para monitorar a que operações foi submetido'
            },
            {
              category: 'Ações Legais',
              examples: 'Estes dados, nomeadamente o seu Nome, Email, Número de Telemóvel, Histórico Médico e Contactos de Emergência poderão ser usados na possibilidade duma ação legal futura*'
            }
          ]
        },
      ]
    },
    {
      title: 'Que fundamento da licitude temos para tratar os seus dados pessoais?',
      contents: [
        {
          type: 'table',
          row1name: 'Fundamento',
          row2name: 'Elaboração',
          rows: [
            {
              category: 'O seu consentimento',
              examples: 'Por escrito ou oralmente'
            },
            {
              category: 'Execução de um contrato',
              examples: 'Por exemplo, executando um contrato de cirurgia que celebrou com o Hospital de Paranhos'
            },
            {
              category: 'Interesses vitais',
              examples: 'Os contactos de emergência são utilizados com base nos interesses vitais'
            },
            {
              category: 'Dados médicos',
              examples: 'Os seus dados médicos são tratados para fins de diagnóstico, prestação de cuidados médicos ou gestão de serviços de saúde'
            },
            {
              category: 'Cumprimento de uma obrigação legal',
              examples: 'Por exemplo, comunicar informações de saúde às autoridades competentes conforme exigido por lei'
            },
            {
              category: 'Interesse legítimo',
              examples: 'Utilizamos os seus dados pessoais para finalidades operacionais, como a melhoria dos nossos serviços, sempre que este interesse não se sobreponha aos seus direitos e liberdades'
            }
          ]
        }
      ]
    },
    {
      title: 'Por quanto tempo conservamos os seus dados pessoais?',
      contents: [
        {
          type: 'text',
          content:
            'O Hospital de Paranhos retém os seus dados pessoais apenas pelo período de tempo estritamente necessário para executar a finalidade para a qual os recolheu, sendo eliminado logo que não sejam úteis. Contudo, os dados pessoais necessários para Ações Legais serão mantidos no sistema por um período de 20 anos.'
        }
      ]
    },
    {
      title: 'Quais são os seus direitos de proteção de dados e como pode exercê-los?',
      contents: [
        {
          type: 'table',
          row1name: 'Direitos',
          row2name: 'Em que consistem',
          rows: [
            {
              category: 'Direito de acesso à informação',
              examples: 'Pode obter a confirmação de quais os seus dados pessoais são tratados por nós, assim como a respetiva informação acerca das finalidades do tratamento ou os seus prazos de conservação.'
            },
            {
              category: 'Direito de retificação',
              examples: 'Pode pedir a retificação ou alteração dos seus dados pessoais se estiverem incorretos ou incompletos.'
            },
            {
              category: 'Direito de apagamento',
              examples: 'Pode pedir o apagamento dos seus dados pessoais, desde que não se verifiquem imposições legais para a sua conservação.'
            },
            {
              category: 'Direito à portabilidade',
              examples: 'Pode receber os dados pessoais, como o seu histórico médico, duma forma estrutura em formato transferível para outras entidades digitais.'
            },
            {
              category: 'Direito à oposição',
              examples: 'Pode opor-se ou retirar o consentimento que deu anteriormente a um tratamento dos seus dados pessoais.'
            }
          ]
        },
        {
          type: 'text',
          content: 'Para exercer qualquer um destes direitos, deve fazê-lo comunicando com o DPO anteriormente estabelecido (ver "Quem é o responsável pelo tratamento dos seus dados pessoais?").'
        },
        {
          type: 'text',
          content: 'Para exercer o Direito de apagamento ou o Direito à portabilidade, pode aceder ao seu perfil pessoal no website. Lá tem as opções necessárias para o fazer.'
        }
      ]
    },
  ];

  // Função para abrir/fechar abas
  toggleTab(index: number) {
    this.activeIndex = this.activeIndex === index ? null : index;
  }
}
